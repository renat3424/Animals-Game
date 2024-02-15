using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float g;
    public abstract void ChangeUI();
    public virtual void Die()
    {
        Game_Manager.instance.enemies.Remove(gameObject.transform.parent.gameObject);
        Destroy(gameObject);

    }
    public virtual bool IsAlive()
    {
        int health = gameObject.transform.parent.GetComponent<HPSystem>().Health;
        return health > 0;
    }
    public virtual void GetDamage(int damage)
    {
        gameObject.transform.parent.GetComponent<HPSystem>().GetDamage(damage);
    }
    public virtual void LookAt(Vector2 position)
    {
       
        gameObject.transform.LookAt(new Vector3(position.x, transform.position.y, position.y));
    }
    public bool InPosition(Vector2 position, float delta)
    {
        float delX = gameObject.transform.position.x - position.y;
        delX = (delX < 0) ? -delX : delX;
        float delY = gameObject.transform.position.z - position.x;
        delY = (delY < 0) ? -delY : delY;
        return delX < delta && delY < delta;
    }
    public virtual void MoveTo(Vector3 direction, float speed) //sets speed 
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        direction = new Vector3(direction.x, 0, direction.z);
        rb.velocity = (direction.normalized * speed) + new Vector3(0, rb.velocity.y, 0);
    }
    public virtual void StopAt(Vector3 position, float delta) //stops object when it is in the given area
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        float delX = gameObject.transform.position.x - position.x;
        delX = (delX < 0) ? -delX : delX;
        float delY = gameObject.transform.position.z - position.y;
        delY = (delY < 0) ? -delY : delY;
        if (delX < delta && delY < delta)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
    public virtual void Stop()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
    }
    public virtual void Attack(GameObject other, int damage) //attacks
    {
        other.GetComponent<HPSystem>().GetDamage(damage);
    }
    public virtual void JumpTo(Vector3 direction, float jumpDistance) //jumps to direction vector passing jump distance;
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        direction = new Vector3(direction.x, 0, direction.z).normalized;
        //calculating y coordinate
        float y = (g * jumpDistance) / (3 * direction.magnitude);
        direction += new Vector3(0, y, 0);
        rb.velocity = direction;
    }
    public virtual void JumpOverProp(Vector3 propPosition, float propHeigth, float delta)  //jumps over prop;
    {
        float hMax = propHeigth + delta;
        float y = Mathf.Sqrt(2 * g * hMax);
        float distanceToProp = (transform.position - propPosition).magnitude;
        float directionModulus = g * distanceToProp / y;
        Vector3 direction = (propPosition - transform.position).normalized * directionModulus + new Vector3(0, y, 0);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = direction;
    }
    public void JumpOn(Vector3 position, float propHeight, float jumpHeight) //jump on a position, with jumpHeight
    {
        if (jumpHeight < propHeight) return;
        Vector3 direction = (transform.position - position).normalized;
        float distanceToProp = (transform.position - position).magnitude;
        //calculating speed
        float h = jumpHeight - propHeight;
        float t = 2 * h / g;
        float speed = distanceToProp / t;
        //calculating y
        float y = Mathf.Sqrt(((g * distanceToProp * distanceToProp) / (2 * speed * speed) + propHeight) * 2 * g);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(direction.x * speed, y, direction.z * speed);
    }
    public void JumpOn(Vector3 position, float propHeight, int speed) //jump on a position, with speed
    {

        Vector3 direction = (transform.position - position).normalized;
        float distanceToProp = (transform.position - position).magnitude;
        //calculating y
        float y = Mathf.Sqrt(((g * distanceToProp * distanceToProp) / (2 * speed * speed) + propHeight) * 2 * g);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(direction.x * speed, y, direction.z * speed);
    }
}
