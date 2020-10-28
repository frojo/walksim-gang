using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMonologue : MonoBehaviour
{

    private AudioSource audio;
    private bool played = false;

    public List<AudioClip> clips;
    public float timeBetweenLines;

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
      if (!played) {
        played = true;
        StartCoroutine(playClipsSequentially());
      }
    }

    private IEnumerator playClipsSequentially()
    {
      foreach (AudioClip clip in clips) {
        audio.clip = clip;

        audio.Play();

        while (audio.isPlaying) {
          yield return null;
        }

        yield return new WaitForSeconds(timeBetweenLines);
      }
    }
}
