using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInformation : MonoBehaviour
{
    public float health = 100;
    public int score = 0;
    public TMP_Text textBox;
    public TMP_Text textBox1;
    public TMP_Text textBox2;
    public Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        textBox.text = score.ToString("0");
        textBox1.text = score.ToString("0");
        textBox2.text = score.ToString("0");
    }
    private void OnTriggerEnter(Collider other) {
        //Debug.Log(other.collider.gameObject.tag);
        if(other.gameObject.CompareTag("Trash"))
        {
            Trash trash = other.transform.GetComponent<Trash>();
            score += trash.value;
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Moon"))
        {
            Obstacle obstacle = other.transform.GetComponent<Obstacle>();
            health -= obstacle.damage;
            healthBar.fillAmount = health / 100;
        }
    }
}
