using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RoundController : MonoBehaviour
{
    public List<Rigidbody2D> bodies;
    public TMP_Text roundText;
    public TMP_Text scoreText;
    public Image image;

    [SerializeField] private SpawnPoints spawn;

    public bool playerControlledRounds = false;

    private float kickForce = 1000f;
    public float elapsedTime = 0;
    private float roundStartTime = 0;
    private Player p;

    private bool startRound = false;


    private bool roundInSession = false;

    // Start is called before the first frame update
    void Start()
    {
        IdentifyPlayer();
        FreezeBodies(true);
        DisplayRoundStartText();
    }

    void Update()
    {
        ButtonPressToSlowTime();
        DisplayScoreText();

        if (!roundInSession)
        {
            if (Input.anyKeyDown && playerControlledRounds)
                StartRound();
        }
        else
        {
            elapsedTime = Time.time - roundStartTime;

            if (!p.alive && playerControlledRounds)
            {
                EndRound();
            }
        }
    }


    public IEnumerator LerpImgCol(Color imgCol, float duration)
    {
        imgCol.a = 0.3f;
        float time = 0;
        Color startValue = image.color;
        while (time < duration)
        {
            image.color = Color.Lerp(startValue, imgCol, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        image.color = imgCol;
    }

    public IEnumerator flashImage(Color imgCol)
    {
        float dur = 0.1f;
        StartCoroutine(LerpImgCol(imgCol, dur));
        yield return new WaitForSecondsRealtime(dur);
        StartCoroutine(LerpImgCol(Color.black, dur));
    }


    void FreezeBodies(bool freeze)
    {
        foreach (Rigidbody2D b in bodies)
        {
            if (freeze)
                b.constraints = RigidbodyConstraints2D.FreezePosition;
            else
                b.constraints = RigidbodyConstraints2D.None;
        }
    }


    void IdentifyPlayer()
    {
        foreach (Rigidbody2D b in bodies)
        {
            if (b.CompareTag("Player"))
                p = b.GetComponent<Player>();
        }
    }

//    IEnumerator SetInitalPos()
//    {
//        List<Transform> init = spawn.GetSpawnPoints(bodies.Count);
//
//        Rigidbody2D playerRb = p.GetComponent<Rigidbody2D>();
//        float time = 0;
//        float duration = 0.3f;
//        while (time < duration)
//        {
//            for (int i = 0; i < bodies.Count; i++)
//            {
//                Rigidbody2D b = bodies[i];
//                Vector2 p0 = b.position;
//                Vector2 p1 = init[i].position;
//                time += Time.deltaTime;
//                
//                Debug.Log("Spawn " + b.name + " at " + init[i].name);
//                
//                if (b == playerRb)
//                    b.transform.position = p1;
//                else
//                    b.MovePosition(Vector2.Lerp(p0, p1, time / duration));
//                
//            }
//            yield return null;
//        }
//    }

    void SetInitalPos()
    {
        List<Transform> init = spawn.GetSpawnPoints(bodies.Count);

        for (int i = 0; i < bodies.Count; i++)
        {
            bodies[i].transform.position = init[i].position;
        }
    }

    void DisplayScoreText()
    {
        scoreText.text = String.Format("Score: {0:00}", p.score);
    }


    void DisplayRoundStartText()
    {
        roundText.text = "Navigate between the two bodies.\nClick to start round.";
    }

    void DisplayRoundRestartText()
    {
        roundText.text = "Restart?";
    }

    void ClearRoundText()
    {
        roundText.text = "";
    }

    public void StartRound()
    {
        roundStartTime = Time.time;
        ClearRoundText();
//        StartCoroutine(SetInitalPos());
        SetInitalPos();
        roundInSession = true;
        FreezeBodies(false);
        KickEnemies();
        Vector2 pt = p.GetComponent<Transform>().position;
        p.ResetPlayer(pt);
    }

    void KickEnemies()
    {
        Vector2 p0 = p.gameObject.transform.position;
        foreach (var b in bodies)
        {
            if (b.CompareTag("Enemy"))
            {
                Vector2 p1 = b.transform.position;
                Vector2 pe = (p0 - p1).normalized;


                Vector3 rot = Quaternion.AngleAxis(90, Vector3.back) * pe;
                int sign = Random.Range(0, 2) * 2 - 1;

                b.AddForce(rot.normalized * kickForce * sign);
            }
        }
    }

    public void EndRound()
    {
        roundInSession = false;
        FreezeBodies(true);
        DisplayRoundRestartText();
    }


    void ButtonPressToSlowTime()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // slow down time from 1 too 0.5
            Time.timeScale = .5f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}