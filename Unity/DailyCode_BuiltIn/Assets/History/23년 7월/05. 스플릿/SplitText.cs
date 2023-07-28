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
        //str = "WhiskeyData|0,25,위스키파일명,파일사이즈,[{타이틀텍스트내용,[{네임명},{브랜드},{알콜},{볼륨},{컨트리},{버라이어티},[{문장" +
        //            "|1,25,위스키파일명,파일사이즈,[{타이틀텍스트내용,[{네임명},{브랜드},{알콜},{볼륨},{컨트리},{버라이어티},[{문장" +
        //            "|2,25,위스키파일명,파일사이즈,[{타이틀텍스트내용,[{네임명},{브랜드},{알콜},{볼륨},{컨트리},{버라이어티},[{문장";

        str = "WhiskeyData|0,25,2071148445.png,6500,[{CHIVAS REGAL,[{CHIVAS REGAL XV 15 YEAR OLD SCOTCH WHISKY},{CHIVAS REGAL},{40%},{1000ml},{},{,[{It is a Scotch\r\nWhiskey brand that\r\nstarted in 1867. \r\nAlong with\r\nBallantine's and\r\nChivas Regal, the\r\nmost famous\r\nblended whiskey\r\nbrands in Korea and\r\naround the world.\r\nImported from\r\nDiageo Korea|1,30,2071148445.png,0,[{CHIVAS\r\nREGAL,[{CHIVAS REGAL XV 15 YEAR OLD SCOTCH WHISKY},{CHIVAS REGAL},{23523E},{235323},{2323T},{2323W,[{DD|2,35,2071145499.png,0,[{GLENMORANGIE,[{GLENMORANGIE ACCORD 12 YEARS OLD 1000ML},{GLENMORANGIE},{DDDWE},{QE},{EEDWT},{WEWEW,[{DD|3,40,2071870971.png,0,[{LAMBAY,[{LAMBAY WHISKEY BLENDED MALT},{LAMBAY},{DDDWE},{QE},{EEDWT},{WEWEW,[{DD|4,45,2072184992.png,0,[{TALISKER,[{TALISKER SURGE},{TALISKER},{DDDWE},{QE},{EEDWT},{WEWEW,[{DD|5,30,2072353350.png,0,[{CAMUS,[{32323},{CAMUS},{DDDWE},{QE},{EEDWT},{WEWEW,[{DD";

        Debug.Log(str);

        Debug.Log(str.Split('|')[1].Split(",[{")[0]); // 0,25,위스키파일명,파일사이즈
        Debug.Log(str.Split('|')[1].Split(",[{")[1]); // 타이틀 텍스트 내용
        Debug.Log(str.Split('|')[1].Split(",[{")[2]); // leftText},{네임명},{브랜드},{알콜},{볼륨},{컨트리},{버라이어티},{rightText

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
        Debug.Log("전송 받은 메시지 : " + data.message);
    }

    //-------------- 신호 체크
    public ORTCPClient client;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 서버쪽으로 데이터 보내기
            client.Send("DispenserOutflowData|2023-07-22 13:28:50,2071148445,20,3");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // 콜바이 레퍼런스 개념확인
            List<string> li1 = new List<string>();
            li1.Add("1");

            List<string> li2 = new List<string>();
            li2 = li1;

            li2.Add("2");

            // 콜바이레퍼런스때문에 연결되어서, L2의 값을 변경해도 L1에도 적용된다.
            foreach (string str in li1) { Debug.Log(str); }
        }
    }
}
