using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using ZTH.UTool;

public class Character : MonoBehaviour
{
    private void FixedUpdate()
    {
        Anim.speed = speed;

        var movement = GetMovement();
        if (movement == Vector2.zero)
        {
            Anim.Play("Idle" + animStateDir);
            RB.velocity = Vector2.zero;
            return;
        }

        if (movement.x > 0) animStateDir = "R";
        if (movement.x < 0) animStateDir = "L";
        if (movement.y > 0) animStateDir = "B";
        if (movement.y < 0) animStateDir = "F";
        Anim.Play("Walk" + animStateDir);
        RB.MovePosition((Vector2)transform.position + speed * Time.fixedDeltaTime * movement);
    }

    private Vector2 GetMovement()
    {
        var h = Input.GetAxisRaw("Horizontal");
        if (h != 0) return new Vector2(h, 0);
        var v = Input.GetAxisRaw("Vertical");
        if (v != 0) return new Vector2(0, v);
        return Vector2.zero;
    }

    [SerializeField] private float speed = 1;

    private string animStateDir;

    private Rigidbody2D RB => transform.Get(ref rb); private Rigidbody2D rb;
    private Animator Anim => transform.Get(ref anim); private Animator anim;
}
