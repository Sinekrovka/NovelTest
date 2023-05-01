using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Effector : MonoBehaviour
{
    public static Effector Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void SetImageColor(Image img, Color color, float time)
    {
        if (time < 0)
        {
            img.color = color;
            img.DOColor(Color.white, Mathf.Abs(time));
        }
        else
        {
            img.DOColor(color, time);
        }
    }

    public void SetImageMovement(RectTransform rect, Vector2 direction, Vector2 endPoint, float time)
    {
        float x = rect.anchoredPosition.x;
        float y = rect.anchoredPosition.y;
        if (direction.x < 0)
        {
            x -= 1400f;
        }
        else if(direction.x>0)
        {
            x += 1400;
        }

        if (direction.y < 0)
        {
            y -= 1200;
        }
        else if(direction.y>0)
        {
            y += 1200;
        }
        
        rect.anchoredPosition = new Vector2(x,y);
        rect.DOAnchorPos(endPoint, time);
    }

    public void SetImageFade(Image img, float time)
    {
        if (time < 0)
        {
            img.DOFade(0, Mathf.Abs(time));
        }
        else
        {
            img.DOFade(1, time);
        }
    }
}
