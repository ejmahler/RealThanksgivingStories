﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhoneSwipe : MonoBehaviour {
    private Mask MaskObject;

    [SerializeField]
    private List<Sprite> _UglyPhotoSprites;
    [SerializeField]
    private Sprite _PocaHottieSprite;

    [SerializeField]
    private Image _TinderPhotoPrefab;

    [SerializeField]
    private FadeToBlackScript _ScreenFadePrefab;

    [SerializeField]
    private string _NextScene;

    [SerializeField]
    private AudioClip[] _SwipeSounds;

    [SerializeField]
    private AudioClip[] _DisapprovalSounds;

    [SerializeField]
    private AudioClip[] _ApprovalSounds;

    [SerializeField]
    private AudioSource _SwipeSource;

    [SerializeField]
    private AudioSource _ApprovalSource;

    Queue<Image> QueuedSwipes = new Queue<Image>();

    enum SwipeResult { Left, Right };
    private SwipeResult LastSwipeResult;

    Image SetupSwipePhoto(Sprite PhotoSprite) {
        Image newThing = Instantiate<Image>(_TinderPhotoPrefab);
        newThing.sprite = PhotoSprite;
        newThing.transform.SetParent(MaskObject.transform, false);
        newThing.transform.SetAsFirstSibling();
        newThing.rectTransform.anchoredPosition3D = Vector3.zero;
        return newThing;
    }

	void Awake () {
        MaskObject = GetComponentInChildren<Mask>();

        foreach(Sprite SpriteToSpawn in _UglyPhotoSprites) {
            QueuedSwipes.Enqueue(SetupSwipePhoto(SpriteToSpawn));
        }
        QueuedSwipes.Enqueue(SetupSwipePhoto(_PocaHottieSprite));
        foreach (Sprite SpriteToSpawn in _UglyPhotoSprites)
        {
            QueuedSwipes.Enqueue(SetupSwipePhoto(SpriteToSpawn));
        }
        QueuedSwipes.Enqueue(SetupSwipePhoto(_PocaHottieSprite));

        StartCoroutine(ManageSwipes());
	}

    IEnumerator ManageSwipes() {
        bool FirstThing = true;
        while (true) {
            Image NextSwipe = QueuedSwipes.Dequeue();
            Sprite CurrentSprite = NextSwipe.sprite;

            // If the queue of remaining things to swipe is empty, add a pocahontas sprite so that she appears infinitely
            if(QueuedSwipes.Count == 0) {
                QueuedSwipes.Enqueue(SetupSwipePhoto(_PocaHottieSprite));
            }

            if (!FirstThing)
            {
                if (NextSwipe.sprite == _PocaHottieSprite && !_ApprovalSource.isPlaying)
                {
                    _ApprovalSource.PlayRandomizedDelayed(0.75f, _ApprovalSounds);
                }
                else
                {
                    _ApprovalSource.PlayRandomizedDelayed(0.75f, _DisapprovalSounds);
                }
            }

            yield return HandleSwipe(NextSwipe);

            _SwipeSource.PlayRandomized(_SwipeSounds);

            if (CurrentSprite == _PocaHottieSprite && LastSwipeResult == SwipeResult.Right) {
                FadeToBlackScript FadeInstance = Instantiate<FadeToBlackScript>(_ScreenFadePrefab);
                FadeInstance.currentStatus = FadeToBlackScript.FadeStatus.FadingToBlack;
                DontDestroyOnLoad(FadeInstance.gameObject);

                yield return new WaitUntil(() => FadeInstance.IsOpaque);
                yield return new WaitForSeconds(1.0f);

                FadeInstance.currentStatus = FadeToBlackScript.FadeStatus.FadingToTransparent;
                SceneManager.LoadScene(_NextScene);
            }
            FirstThing = false;
        }
    }

    IEnumerator HandleSwipe(Image imageWidget) {
        SwipeablePhoto photoComponent = imageWidget.gameObject.AddComponent<SwipeablePhoto>();
        while (true) {
            photoComponent.ResetSwipe();
            yield return new WaitUntil(() => photoComponent.CurrentState == SwipeablePhoto.SwipeState.Dropped);

            if (photoComponent.rectTransform.anchoredPosition.x < -MaskObject.rectTransform.rect.width * 0.4f) {
                LastSwipeResult = SwipeResult.Left;
                Destroy(imageWidget.gameObject);
                yield break;
            }
            else if (photoComponent.rectTransform.anchoredPosition.x > MaskObject.rectTransform.rect.width * 0.4f)
            {
                LastSwipeResult = SwipeResult.Right;
                Destroy(imageWidget.gameObject);
                yield break;
            }

            yield return null;
        }
    }
}
