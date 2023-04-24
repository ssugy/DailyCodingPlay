using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgURLLoaderManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            imgUrlLoad();
        }
    }

    private void imgUrlLoad()
    {
        string[] imgUrls = new string[]
        {
            "https://lrl.kr/data/editor/2012/1608271222.9649yeosu%204k.jpg",
            "https://lrl.kr/data/editor/2012/1608796343.0803wp4190329-busan-wallpapers.jpg",
            "https://lrl.kr/data/editor/2012/1609291704.2462LRL.KR-1.png",
            "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjA5MTBfMjY4%2FMDAxNjYyNzgxNzAxMTg2.Jn9ZW9p5_Mh-vAt8N2u5ctBDHytimcc5b7h7yuZoC90g.0OMt6pv0WYOvyA5Qmi203yicX0BhWj-XG9tWgv2D-qwg.JPEG.dowena91%2F1662781687160.jpg&type=sc960_832",
            "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMzAyMTBfMTcw%2FMDAxNjc2MDI1MzE2MDA5.KIr7-m2VN1-i3P4KL1QP7MnVCtJ9X26zirOr0yFHHWUg.pDnYr8c2FeAw58eVQJN4sbzeobfxeEeYSge4_6nclk4g.PNG.smotherguy%2F3.png&type=sc960_832",
            "https://talkimg.imbc.com/TVianUpload/tvian/TViews/image/2022/03/25/b7647118-5eb5-444d-8404-e18f917741db.jpg",
            "https://talkimg.imbc.com/TVianUpload/tvian/TViews/image/2022/03/25/52a739f5-843a-40cb-9c34-0dbfc88a2e4b.jpg",
            "https://cdn.topstarnews.net/news/photo/202111/14649286_724074_4438.jpg",
            "https://cdn.topstarnews.net/news/photo/202111/14649287_724075_453.jpg",
            "https://cdn.topstarnews.net/news/photo/202111/14649288_724076_4639.jpg",
            "https://cdn.topstarnews.net/news/photo/202111/14649291_724079_4930.jpg",
            "https://upload3.inven.co.kr/upload/2022/03/15/bbs/i16343629296.jpg?MW=800",
            "https://upload3.inven.co.kr/upload/2022/03/15/bbs/i16485681820.jpg?MW=800",
            "https://upload3.inven.co.kr/upload/2022/03/15/bbs/i13770632204.jpg?MW=800",
            "https://upload3.inven.co.kr/upload/2022/03/15/bbs/i14227794140.jpg?MW=800",
            "https://upload3.inven.co.kr/upload/2022/03/15/bbs/i13217371151.jpg?MW=800",
            "https://upload3.inven.co.kr/upload/2022/03/15/bbs/i13377660629.jpg?MW=800",
            "https://upload3.inven.co.kr/upload/2022/03/15/bbs/i14741685047.jpg?MW=800",
            "https://upload3.inven.co.kr/upload/2022/03/15/bbs/i15401683459.jpg?MW=800",
            "https://upload3.inven.co.kr/upload/2022/03/15/bbs/i15104219596.jpg?MW=800",
            "https://upload3.inven.co.kr/upload/2022/03/15/bbs/i15438356083.jpg?MW=800",
            "https://photo.coolenjoy.co.kr/data/editor/1911/Bimg_3b4217120b15ab96fd5bae261be6a0de_thc9.jpg",
            "https://photo.coolenjoy.co.kr/data/editor/1911/Bimg_3b4217120b15ab96fd5bae261be6a0de_jpqo.jpg",
            "https://photo.coolenjoy.co.kr/data/editor/1911/Bimg_3b4217120b15ab96fd5bae261be6a0de_21qy.jpg",
            "https://photo.coolenjoy.co.kr/data/editor/1911/Bimg_3b4217120b15ab96fd5bae261be6a0de_9k1y.jpg",
            "https://photo.coolenjoy.co.kr/data/editor/1911/Bimg_3b4217120b15ab96fd5bae261be6a0de_w39t.jpg",
            "https://pic.filecast.co.kr/3d/a2/3da2a76ea736027b31e41562c49e49ed.jpg",
            "https://i.imgur.com/cSGJJw5.jpg",
        };

        string savePath = FrameworkConstants.MediaFileRoot;
        string savePath2 = Application.dataPath + "/tmp/";
        Debug.Log("시작1");
        StartCoroutine(ImgUrlLoader.DownloadAndSaveImages(imgUrls, savePath2, progress =>
        {
            Debug.Log("시작2");
            if (progress == 1)
            {
                Debug.Log("시작3");
                Debug.Log("download complete !!!");
            }
        }));
    }
}
