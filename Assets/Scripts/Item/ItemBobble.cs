using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBobble : BaseBubble
{

    
    public Color color;
    public Color colorTeam02;
        public Color colorTeam01;
    public object gameObjectPlayer;
    public int team;
    // Start is called before the first frame update
    void Start()
    {
        
    }


 public void SetTeam(int teamid,object gameObject)
{
   team=teamid;
   gameObjectPlayer=gameObject;
}
    
    void Update()
    {

     Conquer();


    }
private void OnCollisionEnter2D(Collision2D collision)
{


    // Check the collided object
    GameObject collidedObject = collision.gameObject;

     Debug.Log("Collided with: " + collidedObject.name);

  if(collidedObject!=gameObjectPlayer){
    // Check if the collided object has the "Player" tag
    if (collidedObject.CompareTag("Player"))
    {
        // Destroy the collided object (the player in this case)
        Destroy(gameObject);
        Debug.Log("DestroyBubble of"+gameObject);
    }
    else if(collidedObject.CompareTag("wall"))
    {
        // Destroy the collided object (the player in this case)
        Destroy(gameObject);
            Debug.Log("DestroyBubble of"+gameObject);
    }
  }

}



    private void Conquer()
    {
 

      if(team==1)
      {
          color=colorTeam01;
      }
      else if(team==2)
      {
          color=colorTeam02;
      }



        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);

        foreach (Collider2D collider in colliders)
        {
            GroundObject groundObject = collider.GetComponent<GroundObject>();
            if (groundObject != null && !groundObject.isWall)
            {
                groundObject.SetTeam(team, color);
            }
        }
    }
}