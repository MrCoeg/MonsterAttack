using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattlesAction : MonoBehaviour
{
    public GameObject notificationsObject;
    public float floatingDuration;
    public void Notify(Sprite activator, string message)
    {
        var notification = Instantiate(notificationsObject, transform);
        var verticalLayoutGroup = notification.GetComponent<VerticalLayoutGroup>();
        var text = notification.GetComponentInChildren<TextMeshProUGUI>();
        text.text = message;

        var image = notification.GetComponentsInChildren<Image>().Last();
        image.sprite = activator;

        Destroy(notification, floatingDuration+0.5f);

        StartCoroutine(UpAnimation(verticalLayoutGroup));

    }

    IEnumerator UpAnimation(VerticalLayoutGroup verticalLayoutGroup)
    {
        var time = 0f;

        while (time < floatingDuration)
        {
            time += Time.deltaTime;
            verticalLayoutGroup.spacing += 0.1f;
            yield return null;
        }

    }

}
