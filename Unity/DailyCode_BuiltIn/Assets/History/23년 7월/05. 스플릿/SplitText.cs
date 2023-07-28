using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SplitText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string str = string.Empty;
        //str = "WhiskeyData|0,25,����Ű���ϸ�,���ϻ�����,[{Ÿ��Ʋ�ؽ�Ʈ����,[{���Ӹ�},{�귣��},{����},{����},{��Ʈ��},{�����̾�Ƽ},[{����" +
        //            "|1,25,����Ű���ϸ�,���ϻ�����,[{Ÿ��Ʋ�ؽ�Ʈ����,[{���Ӹ�},{�귣��},{����},{����},{��Ʈ��},{�����̾�Ƽ},[{����" +
        //            "|2,25,����Ű���ϸ�,���ϻ�����,[{Ÿ��Ʋ�ؽ�Ʈ����,[{���Ӹ�},{�귣��},{����},{����},{��Ʈ��},{�����̾�Ƽ},[{����";

        str = "WhiskeyData|0,25,2071148445.png,6500,[{CHIVAS REGAL,[{CHIVAS REGAL XV 15 YEAR OLD SCOTCH WHISKY},{CHIVAS REGAL},{40%},{1000ml},{},{,[{It is a Scotch\r\nWhiskey brand that\r\nstarted in 1867. \r\nAlong with\r\nBallantine's and\r\nChivas Regal, the\r\nmost famous\r\nblended whiskey\r\nbrands in Korea and\r\naround the world.\r\nImported from\r\nDiageo Korea|1,30,2071148445.png,0,[{CHIVAS\r\nREGAL,[{CHIVAS REGAL XV 15 YEAR OLD SCOTCH WHISKY},{CHIVAS REGAL},{23523E},{235323},{2323T},{2323W,[{DD|2,35,2071145499.png,0,[{GLENMORANGIE,[{GLENMORANGIE ACCORD 12 YEARS OLD 1000ML},{GLENMORANGIE},{DDDWE},{QE},{EEDWT},{WEWEW,[{DD|3,40,2071870971.png,0,[{LAMBAY,[{LAMBAY WHISKEY BLENDED MALT},{LAMBAY},{DDDWE},{QE},{EEDWT},{WEWEW,[{DD|4,45,2072184992.png,0,[{TALISKER,[{TALISKER SURGE},{TALISKER},{DDDWE},{QE},{EEDWT},{WEWEW,[{DD|5,30,2072353350.png,0,[{CAMUS,[{32323},{CAMUS},{DDDWE},{QE},{EEDWT},{WEWEW,[{DD";

        Debug.Log(str);

        Debug.Log(str.Split('|')[1].Split(",[{")[0]); // 0,25,����Ű���ϸ�,���ϻ�����
        Debug.Log(str.Split('|')[1].Split(",[{")[1]); // Ÿ��Ʋ �ؽ�Ʈ ����
        Debug.Log(str.Split('|')[1].Split(",[{")[2]); // leftText},{���Ӹ�},{�귣��},{����},{����},{��Ʈ��},{�����̾�Ƽ},{rightText

        foreach (string str2 in str.Split('|')[1].Split(",[{")[2].Split("},{"))
        {
            Debug.Log(str2);
        }

        string str3 = "D:\\test";
        Debug.Log(str3.Split('\\').Last());
        
    }

    private void OnClientConnect()
    {

    }

    private void OnDataReceived(ORTCPEventParams data)
    {
        Debug.Log("���� ���� �޽��� : " + data.message);
    }

    //-------------- ��ȣ üũ
    public ORTCPClient client;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // ���������� ������ ������
            client.Send("DispenserOutflowData|2023-07-22 13:28:50,2071148445,20,3");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // �ݹ��� ���۷��� ����Ȯ��
            List<string> li1 = new List<string>();
            li1.Add("1");

            List<string> li2 = new List<string>();
            li2 = li1;

            li2.Add("2");

            // �ݹ��̷��۷��������� ����Ǿ, L2�� ���� �����ص� L1���� ����ȴ�.
            foreach (string str in li1) { Debug.Log(str); }
        }
    }
}
