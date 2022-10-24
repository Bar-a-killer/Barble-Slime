using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Play : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] Text hptxt;
    [SerializeField] int maxhp =3;
    [SerializeField] GameObject hpbar;
    [SerializeField] float Jump; 
    [SerializeField] float runspeed = 8;
    int hp = 3;
    bool touch = false;  
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (GetComponent<Rigidbody2D>().gravityScale == 0f)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    transform.Translate(0, runspeed * Time.deltaTime, 0);
                else
                    transform.Translate(0, speed * Time.deltaTime, 0);
            }
                
            else
            {

                if (touch)
                {

                    GetComponent<Rigidbody2D>().AddForce(transform.up * Jump, ForceMode2D.Impulse);
                    GetComponent<Animator>().SetBool("jump", true);
                    touch = false;

                }
                else
                {
                    if (Input.GetKey(KeyCode.D))
                        transform.Translate(speed * Time.deltaTime, 0, 0);
                    else if (Input.GetKey(KeyCode.A))
                        transform.Translate(-speed * Time.deltaTime, 0, 0);
                }


            }

        }
        else if (Input.GetKey(KeyCode.A))
        { 
            if (Input.GetKey(KeyCode.LeftShift))
                transform.Translate(-runspeed * Time.deltaTime, 0, 0);
            else
                transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                transform.Translate(0, -runspeed * Time.deltaTime, 0);
            else
                transform.Translate(0, -speed * Time.deltaTime, 0);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                transform.Translate(runspeed * Time.deltaTime, 0, 0);
            else
                transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts[0].normal.y >= 0.5f)
        {
            GetComponent<Animator>().SetBool("jump", false);
            touch = true;
        }
            

        Checkmap checkmap = new Checkmap();

        //�S�O�I(��)
        if (other.gameObject.tag == "specdot")
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1);
            Usekey usekey = new Usekey();
            usekey.Change_key('t');
        }
        //�S�O�I(��)
        else if (other.gameObject.tag == "specdot0")
        {
            GetComponent<SpriteRenderer> ().color = new Color(1,1,1,1);
            Usekey usekey = new Usekey();
            usekey.Change_key('0');
             
            hpbar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-196.8f-496 * (maxhp - hp) / maxhp/2, 426);


        }
        //�S�O�I(��-���`)
        else if (other.gameObject.tag == "specdot1")
        {
            Usekey usekey = new Usekey();
            GetComponent<SpriteRenderer>().color = new Color(37f/255f, 1, 86f/255f, 1);
            GetComponent<Transform>().position = new Vector2(-24, 0);
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            usekey.Change_key('1');
            maxhp = 0;
            hp = 0;
            //hptxt.text = "Hp" + hp.ToString();
             
             

        }
        //�S�O�I(��-���O)
        if (other.gameObject.tag == "specdot2")
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 1);
            Usekey usekey = new Usekey();
            usekey.Change_key('2');
            if (GetComponent<Rigidbody2D>().gravityScale == 0f)
                GetComponent<Rigidbody2D>().gravityScale = 5f;
            else
            {
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<SpriteRenderer>().color = new Color(37f / 255f, 1, 86f / 255f, 1);
                GetComponent<Animator>().SetBool("jump", false);
            }
                
        }
        //HP�I
        if (other.gameObject.tag == "hpdot")
        {
            hp++;
            if(hp > maxhp) hp = maxhp;
            //hptxt.text = "Hp" + hp.ToString();
             
             
        }
        //enemy
        if (other.gameObject.tag == "enemy")
        {
            hp-=1;
            if (hp <= 0)
            {
                maxhp = 3;
                hp = 3;
                Usekey usekey = new Usekey();
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                GetComponent<Transform>().position = new Vector2(-24, 0);
                usekey.Change_key('1');           
            }
            //hptxt.text = "Hp" + hp.ToString();
             
             

        }
        //�S�O�I���@�q�S�x
        else if (other.gameObject.tag[0] == 's')
        {
            maxhp += 3;
            hp = maxhp;
            //hptxt.text = "Hp" + hp.ToString();
 
             

        }
        hpbar.GetComponent<RectTransform>().sizeDelta = new Vector2(496 * (maxhp - hp) / maxhp, 22);
        hpbar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-196.8f - 496 * (maxhp - hp) / maxhp / 2, 426);

    }

}