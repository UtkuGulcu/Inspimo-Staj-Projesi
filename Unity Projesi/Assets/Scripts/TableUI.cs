using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject hasProgressGameObject;

    private IHasProgress hasProgress;

    private void Awake()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
    }

    private void Start()
    {
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        slider.value = 1f;
        Hide();
    }

    private void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        slider.value = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
