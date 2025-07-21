using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonDirectionRandomizer : MonoBehaviour
{
    [System.Serializable]
    public class DirectionInfo
    {
        public string name;
        public Vector2 direction;
        public float degrees;
        public string piLabel;

        public string ToRandomLabel()
        {
            int pick = Random.Range(0, 3);
            switch (pick)
            {
                case 0: return direction.ToString("F1");
                case 1: return piLabel;
                case 2: return degrees + "°";
                default: return name;
            }
        }
    }

    [System.Serializable]
    public class DirectionButton
    {
        public Button button;
        public DirectionInfo assignedDirection;
        [HideInInspector] public float cooldownTimer = 0f;
    }

    [Header("Direction Setup")]
    public List<DirectionInfo> directions = new List<DirectionInfo>();
    public List<DirectionButton> buttons = new List<DirectionButton>();

    [Header("Cooldown Settings")]
    public float buttonCooldownDuration = 1.0f;

    void Start()
    {
        ShuffleDirections();
        AssignToButtons();
    }

    void Update()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].cooldownTimer > 0f)
            {
                buttons[i].cooldownTimer -= Time.deltaTime;
                if (buttons[i].cooldownTimer <= 0f)
                {
                    UpdateButtonState(i);
                }
            }
        }
    }

    void ShuffleDirections()
    {
        for (int i = directions.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = directions[i];
            directions[i] = directions[j];
            directions[j] = temp;
        }
    }

    void AssignToButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            var dirInfo = directions[i];
            buttons[i].assignedDirection = dirInfo;

            // Set button label
            TextMeshProUGUI label = buttons[i].button.GetComponentInChildren<TextMeshProUGUI>();
            label.text = dirInfo.ToRandomLabel();

            int index = i; // Capture for closure
            Button btn = buttons[i].button;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                if (buttons[index].cooldownTimer <= 0f)
                {
                    FindObjectOfType<BulletShooter>().ShootBullet(dirInfo.direction);
                    buttons[index].cooldownTimer = buttonCooldownDuration;
                    UpdateButtonState(index);
                }
            });

            UpdateButtonState(i);
        }
    }

    void UpdateButtonState(int index)
    {
        Button btn = buttons[index].button;
        bool onCooldown = buttons[index].cooldownTimer > 0f;

        btn.interactable = !onCooldown;

        var text = btn.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.alpha = onCooldown ? 0.5f : 1f;
        }
    }

    public void Reshuffle()
    {
        ShuffleDirections();
        AssignToButtons();
    }
}
