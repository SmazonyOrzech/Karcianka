using System;
using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text descrition;
    [SerializeField] private TMP_Text mana;
    [SerializeField] private SpriteRenderer imageSR;
    [SerializeField] private GameObject wrapper;
    [SerializeField] private LayerMask dropLayer;
    private Quaternion rotation;

    public Card Card { get; private set; }
    private Vector3 dragStartPosition;
    private Quaternion dragStartRotation;
    private Vector3 prevoiusPosition;
    public void Setup(Card card)
    {
        Card = card;
        title.text = card.Title;
        descrition.text = card.Description;
        imageSR.sprite = card.Image;
        mana.text = card.Mana.ToString();
    }
    private void OnMouseEnter()
    {
        if (!Interactions.Instance.PlayerCanHover())
            return;
        wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2, 0);
        CardViewHoverSystem.Instance.Show(Card, pos);
    }
    private void OnMouseExit()
    {
        if (!Interactions.Instance.PlayerCanHover())
            return;
        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }
    private void OnMouseDown()
    {
        if (!Interactions.Instance.PlayerCanHover())
            return;
        if (Card.ManualTargetEffect != null)
        {
            ManualTargetSystem.Instance.StartTargeting(transform.position);
        }
        else
        {
            Interactions.Instance.PlayerIsDragging = true;
            wrapper.SetActive(true);
            CardViewHoverSystem.Instance.Hide();
            dragStartPosition = transform.position;
            dragStartRotation = transform.rotation;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
            prevoiusPosition = MouseUtil.GetMousePositionInWorldSpace(-1); 
        }
    }
    private void OnMouseDrag()
    {
        if (!Interactions.Instance.PlayerCanInteract())
            return;
        if (Card.ManualTargetEffect != null) 
            return;
        Vector3 mousePos = MouseUtil.GetMousePositionInWorldSpace(-1);
        Vector3 offset = mousePos - transform.position;
        float rotationX = Mathf.Clamp(-offset.y / 2f, -1f, 1f) * 15f;
        float rotationY = Mathf.Clamp(-offset.x / 2f, -1f, 1f) * 15f;

        transform.position = mousePos;
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
    private void OnMouseUp()
    {
        if (!Interactions.Instance.PlayerCanInteract())
            return;
        if(Card.ManualTargetEffect != null)
        {
            EnemyView target = ManualTargetSystem.Instance.EndTargeting(MouseUtil.GetMousePositionInWorldSpace(-1));
            if(target != null
                && ManaSystem.Instance.HasEnoughMana(Card.Mana))
            {
                PlayCardGA playCardGA = new(Card, target);
                ActionSystem.Instance.Perform(playCardGA);
            }
        }
        else
        {
            if (ManaSystem.Instance.HasEnoughMana(Card.Mana)
            && Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, 10f, dropLayer))
            {
                PlayCardGA playCardGA = new(Card);
                ActionSystem.Instance.Perform(playCardGA);
            }
            else
            {
                transform.position = dragStartPosition;
                transform.rotation = dragStartRotation;
            }
            Interactions.Instance.PlayerIsDragging = false; 
        }
    }
}
