using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //黒石,白石,ブロックのプレハブを入れておく…これをフィールドに生成する！
    [SerializeField] GameObject blackStone, whiteStone, Block;

    //黒石,白石,空,ブロックを示す数字を入れておく CheckClear.CheckStatus();で判別のため使用する
    public static int BLACK = 1,WHITE = -1,EMPTY = 0,BLOCK = 2;

    //クリックしたところを受け取る 座標管理のために白ターンと黒ターンのクリック位置を分割
    RaycastHit hitWhite,hitBlack,hit,hitWhat;
    
    //座標管理のための二次元配列 
    //[横,縦] CheckClear.CheckStatus()でオブジェクトが置いてあるかどうか判断するときに使います
    public int[,] position = new int[5, 5];

    //白プレイヤーのターンかどうか判断する 白だったらtrue,黒だったらfalseを返す。
    //trueの場合はwhiteSetStone()を実行。falseの場合はblackSetStone()を実行。
    bool isPlayerTurn = true;

    //クリックした座標を入れておくTextオブジェクト
    [SerializeField] Text selectPos;

    //白のX,Z座標、黒のX,Z座標
    int posWX, posWZ, posBX, posBZ;

    //碁石を置いたら+1する。2になったらどちらのターンも終わったという事になる
    //そしたらCanSetPos();でクリックされた座標にInstantiateする。
    int count = 0;
    
    //今どっちのターンか,終了宣言,どっちが勝ったかを入れておくText型
    [SerializeField] Text whichTurn,end,winner;

    //最低碁石はこの位置より上に置いてほしいよね～というもの。
    //CanSetPos();でInstantiateする時に使います
    [SerializeField] float yPosition = 0.37f;

    /*endingTextは「そこまで！」
    　notClickerはクリックしないで次に進もうとしたとき
      selectPosTextはクリックした座標
      notCanGOはそこにもう碁石やらブロックが置かれてるとき
      resultは結果を表示するときに使う。
      SetActive()を使いたいのでGameObjectで取得します
    */
    public GameObject endingText,notClicker,selectPosText,notCanGO,result;

    //クリックされたマスに何もなく、次のターンに進めるかどうかを判定する
    //もしなんか入ってたらfalseになって次のターンに進めない。
    //ClickButtonでも使います。
    public bool canGoNext = true;

    //どこかの座標がクリックされたか判定する。
    //どこもクリックされてなかったらfalseになって進めない。
    //ClickButtonでも使います。
    public bool isClick;

    //どこかの座標がクリックされたら判定します。
    //どこかクリックされたら、他の場所がクリックされたとしても反応しません。
    //CancelButton();や、ターンが変わったときにはtrueになります。
    public bool notToClick = false;

    //3つ並んだらtrueにします。
    //主にUpdate();の中で使います。
    bool battleEnd = false;

    //制限時間を入れておくテキスト
    [SerializeField] Text timeText;

    //実際の制限時間
    public float countDownTime = 15f;



    //効果音を入れておきます
    public AudioSource audioSource;
    public AudioClip blockOki;//ブロックを置いたときの効果音
    public AudioClip stoneOki;//石を置いたときの効果音
    public AudioClip zahyoOki;//座標を選択したときの効果音
    public AudioClip buttonClick;//ボタンをクリックしたときの効果音
    public AudioClip ending;//試合が終了した時のゴング
    public AudioClip notClick;//クリックしてない
    public AudioClip cancel;//配置キャンセル

    //シングルトン化
    public static GameManager instance;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //endingTextは「そこまで！」
        endingText =  GameObject.Find("END");
        endingText.SetActive(false);

        //notClickerはクリックしないで次に進もうとしたとき
        notClicker = GameObject.Find("notClick");
        notClicker.SetActive(false);

        //selectPosTextはクリックした座標
        selectPosText = GameObject.Find("selectPos");
        selectPosText.SetActive(false);

        //notCanGOはそこにもう碁石やらブロックが置かれてるとき
        notCanGO = GameObject.Find("notCanGo");
        notCanGO.SetActive(false);

        //SetActive()を使いたいのでGameObjectで取得します
        result = GameObject.Find("Result");
        result.SetActive(false);    
    }

    void Update()
    {
       
        countDownTime -= Time.deltaTime;
        timeText.text = countDownTime.ToString("f0");
        if(countDownTime <= 0)
        {
            countDownTime = 0;
            

            if (!battleEnd)
            {
                if (isPlayerTurn)
                {
                    endingText.SetActive(true);//そこまでを表示
                    end.text = "そこまで！";
                    result.SetActive(true);//リザルトを表示
                    winner.text = "黒の勝ち！";
                    battleEnd = true;//試合が終わったので状態チェック判定を切る
                    audioSource.PlayOneShot(ending);//カンカンカンカン
                    
                }
                else
                {
                    endingText.SetActive(true);//そこまでを表示
                    end.text = "そこまで！";
                    result.SetActive(true);//リザルトを表示
                    winner.text = "白の勝ち！";
                    battleEnd = true;//試合が終わったので状態チェック判定を切る
                    audioSource.PlayOneShot(ending);//カンカンカンカン
                    
                }
            }
            
        }


        if (!battleEnd)
        {
            
            if (CheckClear.CheckStatus(WHITE))//白の状態をチェック
            {
                if (CheckClear.CheckStatus(BLACK))//もし同時に黒もそろってたら引き分け
                {
                    endingText.SetActive(true);//そこまでを表示
                    end.text = "そこまで！";
                    result.SetActive(true);//リザルトを表示
                    winner.text = "ひきわけ";
                    battleEnd = true;//試合が終わったので状態チェック判定を切る
                    audioSource.PlayOneShot(ending);//カンカンカンカン
                    countDownTime = 0;
                }
                else//もしそろってなかったら白の勝ち
                {                   
                    endingText.SetActive(true);//そこまでを表示
                    end.text = "そこまで！";
                    result.SetActive(true);//リザルトを表示
                    winner.text = "白の勝ち！";
                    battleEnd = true;//試合が終わったので状態チェック判定を切る
                    audioSource.PlayOneShot(ending);//カンカンカンカン
                    countDownTime = 0;
                }

                return;
            }
            if (CheckClear.CheckStatus(BLACK))//黒の状態をチェック
            {
                if (!CheckClear.CheckStatus(WHITE))//もし白がそろってなかったら
                {

                    endingText.SetActive(true);//そこまでを表示
                    end.text = "そこまで！";
                    result.SetActive(true);//リザルトを表示
                    winner.text = "黒の勝ち！";
                    battleEnd = true;//試合が終わったので状態チェック判定を切る
                    audioSource.PlayOneShot(ending);//カンカンカンカン
                    countDownTime = 0;
                }
                return;
            }
        }

        if (isPlayerTurn)
            {
                whiteSetStone();
            }
            else
            {
                blackSetStone();
            }

        if (count == 2)
        {
            CanSetPos();
            count = 0;
        }
    }


    //OnClick();で呼び出す。
    //ターンを変える
    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        if (isPlayerTurn)
        {
            whichTurn.text = "白のターン";
            notToClick = false;
            audioSource.PlayOneShot(buttonClick);
        }
        else
        {
            whichTurn.text = "黒のターン";
            notToClick = false;
            audioSource.PlayOneShot(buttonClick);
        }
        count++;
    }

    public void whiteSetStone()//白石の挙動
        /*具体的には白のターンになったら起動するもの。
         * 白のターン中にクリックされた座標(にあるオブジェクトのx,z値)を取得
         * もしそこに何も入ってなかったらクリックされた座標を表示
         * 入ってたらエラーメッセージを返す
         */
    {
        if (Input.GetMouseButtonDown(0) && !notToClick)
        {
            notCanGO.SetActive(false);
            notClicker.SetActive(false);

            Vector3 pos1 = Input.mousePosition;//マウスのポジションを得る
            Ray ray1 = Camera.main.ScreenPointToRay(pos1);//Rayを飛ばす

            if(Physics.Raycast(ray1,out hitWhite))
            {
                string tag = hitWhite.collider.gameObject.tag;

                if (tag == "Cubes")
                {
                    Ray ray = ray1;
                    if (Physics.Raycast(ray, out hitWhite))
                    {
                        selectPosText.SetActive(true);
                        isClick = true;
                        int posX = (int)hitWhite.collider.gameObject.transform.position.x;//座標に変換
                        int posZ = (int)hitWhite.collider.gameObject.transform.position.z;//座標に変換
                        Debug.Log(hitWhite.collider.gameObject.tag);


                        if (position[posZ, posX] == EMPTY)//もし中身が空だったら
                        {
                            notCanGO.SetActive(false);
                            posWZ = posZ;
                            posWX = posX;
                            selectPos.text = "(" + posWZ + "," + posWX + ")";//クリックした座標を表示
                            audioSource.PlayOneShot(zahyoOki);
                            canGoNext = true;
                            notToClick = true;
                        }
                        else
                        {
                            canGoNext = false;
                        }

                    }
                }
                else if(tag == "Others")
                {
                    Debug.Log(hitWhite.collider.gameObject.tag);
                }
        }

    }

}
            



    public void blackSetStone()//黒石の挙動
    {
        if (Input.GetMouseButtonDown(0) && !notToClick)
        //もし白のターンだったら
        {
            notClicker.SetActive(false);
            notCanGO.SetActive(false);
            
            Vector3 pos1 = Input.mousePosition;//マウスのポジションを得る
            Ray ray1 = Camera.main.ScreenPointToRay(pos1);//Rayを飛ばす

            if(Physics.Raycast(ray1,out hitBlack))
            {

                string tag = hitBlack.collider.gameObject.tag;
                
                if(tag == "Cubes")
                {
                    Ray ray = ray1;

                    if(Physics.Raycast(ray,out hitBlack))
                    {

                            selectPosText.SetActive(true);
                            isClick = true;
                            int posX = (int)hitBlack.collider.gameObject.transform.position.x;//座標に変換
                            int posZ = (int)hitBlack.collider.gameObject.transform.position.z;//座標に変換


                            if (position[posZ, posX] == EMPTY)
                            {
                                notCanGO.SetActive(false);
                                canGoNext = true;
                                posBZ = posZ;
                                posBX = posX;
                                selectPos.text = "(" + posBZ + "," + posBX + ")";
                                audioSource.PlayOneShot(zahyoOki);
                                notToClick = true;
                            }
                            else
                            {

                                canGoNext = false;
                            }
                        }
                    }
            }
            else if (tag == "Others")
            {
                Debug.Log(hitWhite.collider.gameObject.tag);
            }

        }

                    
                    
          }
 
            
        

    


    public void OnClickSound()//ボタンをクリックした時に流れる曲
    {
        audioSource.PlayOneShot(buttonClick);
    }


    public void CanSetPos()//クリックした座標に
    {
        

        if(posBX == posWX && posBZ == posWZ)//もし黒と白の座標が同じだったら
        {
            GameObject blocks = Instantiate(Block);//ブロックを生成
            blocks.transform.position = hitWhite.collider.gameObject.transform.position;

            if(blocks.transform.position.y < 0.37f)//もし生成された時にyが0.37fより小さかったら上に修正
            {
                blocks.transform.Translate(0, yPosition, 0);
            }

            position[posBX, posBZ] = BLOCK;//座標にブロック追加
            audioSource.PlayOneShot(blockOki);

        }
        else
        {
            position[posWZ, posWX] = WHITE;

            GameObject wStone = Instantiate(whiteStone);
            wStone.transform.position = hitWhite.collider.gameObject.transform.position;
            if (wStone.transform.position.y < 0.37f)
            {
                wStone.transform.Translate(0, yPosition, 0);
            }


            position[posBZ, posBX] = BLACK;

            GameObject bStone = Instantiate(blackStone);
            bStone.transform.position = hitBlack.collider.gameObject.transform.position;
            if (bStone.transform.position.y < 0.37f)
            {
                bStone.transform.Translate(0, yPosition, 0);
            }
            audioSource.PlayOneShot(stoneOki);
        }
    }


}
