using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMonologue : MonoBehaviour
{

    //yeah this is hacky as fuck but it's late
    public TransitionWorlds player;

    private AudioSource audio;
    private bool played = false;
    private int currentLine = 0;

    public List<AudioClip> clips;
    public float pauseBetweenLines;

    // Start is called before the first frame update
    void Start()
    {
      audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
      if (player.talkingGhost) {
        return;
      }

      if (!played) {
        played = true;
        player.talkingGhost = this;

        StartCoroutine(playClipsSequentially(currentLine));
      }
    }

    public void ContinueMonologue() {
        StartCoroutine(playClipsSequentially(currentLine));
    }

    private IEnumerator playClipsSequentially(int start)
    {

      for (int i = start; i < clips.Count; i++) {
        audio.clip = clips[i];

        audio.Play();

        while (audio.isPlaying) {
          yield return null;
        }
        
        currentLine++;

        yield return new WaitForSeconds(pauseBetweenLines);
      }

      player.talkingGhost = null;
    }
}
