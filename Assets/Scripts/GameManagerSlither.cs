using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Nakama;

public class GameManagerSlither : MonoBehaviour
{
    public static GameManagerSlither instance;

    public GameObject playerSnake;
    public GameObject mainMenuCanvas;
//    public GameObject InGameJoystickCanvas;

    public Text InGameLenghtText;
    public Text GameOverLenghtText;

    public ColorTemplate[] colorTemplates;

    public Material[] snakeMaterials;
    public Material[] snakeMaterials2;
	public Sprite[] faceImgs;
	public Sprite[] mouthImgs;

    public Material snakeBotMaterial;


//    public GameObject loading, netSlowPop, challengeInfo, challengeInfoPlayArea, pausePop;
	public GameObject pausePop;
    public static int levelNumber;
    public static int selectedSnakeIndex = 0;
    public static int myPlayerMass;
    public static int joyStickType = 2;
    public static bool startWithBigSize;


    public static bool cameFromPlayArea;

    public string onlinePlayerNamesList = "Arrow9 \nFate\nShadow9\nMoon Man06Ox\nChewbacca\nHilariousVader\nYOURnameHERE\nOneSmugPug\nTheDestroyer\nJedterminator\nChampOnTheGo\nIAMTHEREAPER\nTrumpOnTwos\nGameTruce76\nSniperLyfe\nAssassinReject\nComeSweetDeath\nFutureZombie\nMassiveVoid\nRed Testament\nSed\n45645Damas\nLance\nSele\n9Danon4354\nLawe3\n4Shan\nDarel\nCyborg\nKiller\njamesbond\nBigMacSandWitch\nRumplestiltskin\nWarningLowBattery\nPervertPeewee\nMindBuggle\nTheNastyPasty\nSupraNova\nEraZorA\nRockYourDead\nKitEnchanted\nBulletz4Breakfast\nBadBaneCat\nBigBlueCheese\nDeadMonkeyz\nJediAnnihilator\nPFCbulletsponge\nTheJDM Monkey\nSedge\nDamon\n34Land\n6Selig\nDante\nLay\nShap0\nAssasin\nFlame\nSilver\nFuckoffCupid\nKingPopeye\nItalianGump\nMouseRatRockBand\nWindyGod\nMindController\nKoolstack\nIamRage\nRapidCrocs\nWitchingHour\nIamchaakra\nSilentWraith\nAtomicKnuckles\nConmanCometh\nHardly Danger\nMortalMonkey\nSavage Palooka\nAbhi4564\nHagan\nPeot3\nAdam\nHalda\nertPerry\nAdny\nDark\nKnightrider\ndestroyall\nZombiesChef\nMexicanDjango\nGranny4theWIN\nTubbyCandyHoarder\nIonicHound\nMurderSheWrote\nZombiesareback\nManOntheJungle\nUnDeadAlive\nNoNoob\nBigDamnHero\nIndominusRexxx\nBigMastadon\nDevilsreject7\nKindaHomicidal\nPheonixAlcander\nTheNameless24\nAbhil\n34Hagen34\n343Peota345\nAdar\nHale\nPers\nAed90\nAvenger\nDeath\nFrozen\nMist\nSinister\ndestinymaster\nAnheuser\nMasterKeef\nChubsChubs\nSpiderPig\nWeinerstein\nPookieChips\nTickleMeElmo\nGotASegway\nElactixNova\nGlobal meltdown\nImagineYOU\nExcalibur101\nSmartplatypus\nGunDown\nChaosMourn\nFruzzerTrooper\nRecoilboomerang0101\nPushingHeaven19\nMondayGUNS\nBeatMEdead\nBorgCollective\nLaidtoRest\nBloodyAssault\nAzogtheDefiler\natomsmasher13\nBoooDave\nCosmicFunkSquad\nEasy R1der\nHomelessPower\nKlownFaceKilla\nMostHaunted\nPiddleMinger\nSecret System\nThePhoenix1906\nDalla\nDalyn\nLalo\nLamin\n4354Segod\nSelby\n3Danie\nDanny\nLaw\n0Lawa\nShae\nSham8\n342Darby0\nDarek\nDemon\nNeo\ngameblast\nBoomhauer\nFlandersFlannels\nTheMustardCat\nLookWhatICanDo\nLeSpank\nJaredboom\nBloodofraven\nFlominateDominion\nCreekSalvatore\nHelloCREEP\nIronMAN77\nALeperMessiah\nBrooklynSpartan\nEliteCommandoV3\nkronosaurus\nPocketMobsters\nTheRealSkywolf\n345Haep46\nPeola\nAda\nHal\n90Perri2\n3435Adit\nHalsy\nBlaze\niMGhost \nSlayer\nJolly Roger\nPorkChopChop\nHomerPoopson\nEatingHawaiianPizza\nToxicCharger\nSkyxQueen\nTornadoSonic\nRawrGuard\nUnderMeYourDead\nNinjaTrick\nCullingCard\nFightClubAlum\nBaseKillerWolf\nCuddlyPervert\nIceColdCash\nMrVengeance81\nSweDishMaid\n3Haele\n32Peoht\nAcer\nHain345\nPerl\nAden\nHalse0\nDoom777\nNightmare\niamfromUS\nBigAssKiller\nShePoopie\nDnknDonuts\nIPlayFarmHeroes\nOceanstar11\nTrickordie\nKnivesUinBack\nMagicKen\nIntoTheAbyss\nDieOrBye\nXenomorphing\nALtEREvil\nBrownstone80\nElitistBastard\nLawOfTheStreets\nProteusZero\nTheSwissNavy\nLake\nSeger\nDane\n8Laur\nShad\nDarb\nLeal\n32Bodhi\nGreat\nSnipershot\nBelchMerchant\nChumTheReaper\nWillyWizzer\nUnzippedStraw\nSwiftSpeed\nChadkitten\nVolcanicEruption\nAngel123\nDodger2point0\nShaolinKingFu\nTheFinalCountdown\nKillSwitch\nBeasthunt\nCunningLynguist\nIllegalPete\nNanosoldier123\nThePenetrator\nLaine\nSegar\nDana\nLang\n3Sha\n324Daran6\nLeah\nCaptain\nInfinite\ngamerz\nLeonidas\nRufioRoofie\nAwesomeHobo\nVaguelyCynical\nSwiftFox\nStripyrex\nApocalypseMan\nCrazedMaaana\nChromeIsHere\nTheORacle\nSeekNDstroy\nExecuteElectrocute\nBestGamerPeriod\nDaemonocracy\nSoulessImperator\nNeptunePirate\nTheGreatWisecow\nPeoda564\n345Acca353\nHaig\nPercy34\n0Addi343\nHaley\n754Pete\nDoctor\nNinjamaster\nKillertrainz\nMustacheMopol\nFrodoSaggins\nEdgarAllenPoo\nTwoandTwomakes5\nSnowman\nQuickandsilver\nSwords4aSAMURAI\nFreeaakShow\nFighter0Jack\nOrbit1567\nTylerDurden\nAnimeReaper05\nBulletStormSunday\nEvilTrance\nLtCommanderWorf\nRainbowSeven\nCombustion\nPeofa\nAce56\n2Haily\nPeris\nAddy\nHall\n676Peter\nEnigma\nOutlaw\nCRAZYPOOCHES\nCaptain Crunch\nFrogger\nPickingBoogers\nTheBraveChicken\nSinRostro\nBangarmor\nBeastkiller\nTheIllusion\nTheGreatAlex\nTHEsun20\nPennywiseTheClown\nApocalypse3434\nChuckatommy\nFarewellToKings\nMaliciousMutant\nReanimator\nLain\nSeely13\nDan\nLane45645\nSera\nDar\nLeaf9\nShaw\nFallen\nSeeker\nCrazy Anti\nAncientPablo\nGoochieGoonie\nBeanieWeenees\nOneLoneClone\nAfricaGalactic\nNeed4andre\nNaturalbornwinner\nSniperGotYou\nParanoiaSyndicate\nBlueTears\nBluntMachete\nArcticBlade\nColdSystem\nFearlessScorpio\nManBEarPiG1019\nRecoveryShinobi\n56Penta\nAbo\nHagly\nPerce\nAdda\nHalen\nPerye\nAedhert";
    public string offlinePlayerNamesList = "Aadi333\nDace93\n1978Dunn\nLabor\nPeada\nSceld\nAaric\nswdDacey77\nDuve\nLaca\nPears\nSceot\nAarif777\nDack\nDyer999\nLach\nPeck\nScot\n156Aaron\nDado2017\nDyk\nLache\n25Peden\nScott\nAayan2019\nDaeda\n49Hab\nLad\nsdPeers1978\nScout\n36Abba\nDael\nHaca\nLadd\n45Pefen\nSean\ndsfAbbas\nDain\n78Hacca\nLadda\nPell\nSearu\n5Abbe\nDaine\nHada156\nLadde\n48Pen\n3Sebbe\nAbbey25\nDalan\nHadd\nLaddy\n4564Penda49\nSebbi\nAbbot\nDalas\n564Haden36\nLaec\nPeni\nSecca\n46Abby45\nDale\nHadly\n13Lafa\n32Penly78\nSecg\nAbel\nDalen\nHadon5\nLagot\n353Penn\nSecga\n34Abell48\nDaley\n32Haeda\nHama\nLear\n8778Peton\nShel\n3Aega\nDaren\nHamil3435\nLed\n345Petr\nShep\nAelf\nDarik4\nHamo\nLee\nPetyr\n33Siby\nAesc754\nDarin\nHamon\nLele\nPeufa\nSid\n766Ahed676\nDaron\nHana\n4Len\nPhil\nSige\nAin8778\n23Dart\nHand\nLena\nPica\nSijo\n56876Ajax345\nDaryl\nHank\nLenn\nPice\n65Simo\n453Akil\nDash33\nHanly\nLeo\nPicel\nSion\n234Akki\nDaud\nHappy766\nLeof\nPidda\nSith\nAkon\n234Dave\n54Harac\nLeon23\nPiers\nSiva\nAl\n433Davy\nHardy56876\nLes\nPil\nSky\nAlan\nDawe\n5767Hare453\nLevi\nPila\nSkye\nAlao\nDax\nHarl234\n86Levo\nPit\nSonu\n56Alca\nDay\nHarly\nLew234\n5676Pitt\nSped\nAlda\nDean\nHarry\nLex433\nPocg\nStan\n856Aldo\nDeaw\nHart\nLiam\n56Poe5767\nStar\nAlec\nDee\n588Harte\nLian\nPoll\nStea86\nAlen\nDeen\nHasty\nLida\nPort\nStem\nAler5676\n54Deep\nHasu\nLile\n45Poss\nStew\nAlex\nDeke\nHaven856\nLill\nPrim\nStoc\nAlf\nDel\nHavin\n54Lin\nPuca588\nStod\nAlfy\nDell\n65Havyn\nLinc\nPuda\nStok\nAlgy\nDen\n546Hawke\nLind\nPun\nStu\n568568Alik45\nDene\nHawly\nLine\nPuna\nStut\nAlin\nDent\nHeca\n96Link\nPusa\nSuan\n54All\nDeon\nHefa\nLinn\nPuta65\nSubh\nAlmo\nDeor\nHega\n7686Lion\nPyn546\nAloc\nDes\nHelp\nPacca568568\n678Pyt\nAlon\nDev\nHeri\nPace\nSaaky\nAlva\n768Dex\nHern96\nPacey\nSaam\nAlvy\nDeyn\nHero\nPadda\nSaby\nAmbr\n345Dez\nHew\nPaden\nSadoc7686\nAmer\nDh\nHewe\nPaecc\nSadok\nAmma678\n35Dhe\nHid\nPaen\nSaer\nAmos\nDica\nHide768\nPaga\nSagar\nAmyr\nDick\nHill\n534Pagan\nSage\nAndy\nDik\nHlud345\nPage\n34Saher\nAnel\nDikk\nHob\nPaige\n435Saif\nAnit\nDino\nHoc35\nPaine\nSaige\nAnn\nDion\n34Hoca\nPak\nSail\nAnna\nDirk\nHod\n354Paley\nSajin534\nAnno\nDivy\nHoel\nPall\nSakul\nAnta34\n345Dix\nHofa\nPapa\nSalal\nAray435\nDob\nHogg\n354Paris\nSaleh\nArch\nDoc\nHoks\nPark34\nSalsa\nAric\nDocc\nHolt\nParke\nSam354\n534Arif\nDoda\nHowe\nParle\nSamer\nArik\nDodd\nHoyt345\nParr\nSamin\nAriq\n34Dola\nHroc\nParry\nSammy354\nArlo\nDome\nHuca\nParth\nSamy\nArly\nDomo\n345Hud\nPasco\nSanam\nArt\nDon534\nHue\nPassa\nSandy\nArto\nDonn\nHuey\nPat\nSaran\nArul\nDore\nHugh34\nPate\n34Sasa\nArun\nDrew\nHugi\nPaten\nSasha\n53Asa\nDru\nHugo\nPati345\nSatuc\nAsh\nDrue\nHume\nPatin\n34Saven\nAten\nDud\nHuna\nPaton\nSavio\nAtiq\nDuda\nHund\nPatta\nSawal\nAxa34\nDudd\nHunt\n35Paul\nSawin\nAyan\nDudu53\nHurl\nPax\nSaxan\nAyaz\nDuff\nHuw\nPaxon\nSaxe\nAyer34\nDuke\nHy\nPayn\nSaxen\nAzaz\nDull\nHyde\nPayne\nSaxon\nDabbs\nDuna\nLabi\nPeace\nScef35";

    public string[] onlinePlayerNamesListNew;
    public string[] offlinePlayerNamesListNew;

    public string[] AIOfflinePlayrNames;
    public string[] AIOnlinePlayrNames;
    public int AIPlayerNameIndex;
    public static bool isOfflineMode = true;
    public static bool isForceOfflineMode;
    public List<GameObject> playersListLB;
    public List<GameObject> playersNameListLB;
	public List<GameObject> playersLBGlow;
	public Text myPlayerRankTxt;
	public GameObject LBList;

    public GameObject loadingImg;
    public GameObject noNetPopObj, noNetPop;

    public static float version = 2.5f;

    public static int challengeModeType;


	public int enemiesKilledCount;
	public GameObject challengeModeInfoPop;
	public Text playTimeDisplay;
	public Text challengeModeInfo;
	public float currentPlayTime;
	public bool isTargetReached;
	public bool isPlayerBlasted;
	public bool isStartGamePlay;
	public List<Transform> myPlayerSpawnPoses;
//	public Image boosterFillBar;
	public AudioSource playerBoosterSound;
	public string myPlayerName;
	public int myScore;
	public int myPlayerRank;
	public Animation camAnim;
	public static bool ShowVideoInResultPage;
	public bool IsEnableWatchVideoToResume;
	public GameObject myPlayerParent;

	public GameObject shootBtn;

//    public static void setTotalSnakesCountTxt(int countt)
//    {
//        instance.TotalSnakesCountTxt.text = countt.ToStringBtmFast();
//    }
//
//    public static void setInCamSnakesCountTxt(int countt)
//    {
//        instance.InGameSnakesCountTxt.text = countt.ToStringBtmFast();
//    }
//    private static int DyingSankesCount = 0;
//    public static void setDyingSnakesCountTxt(int countt)
//    {
//        DyingSankesCount += countt;
//        if (DyingSankesCount < 0)
//            DyingSankesCount = 1;
//        instance.DyingSnakesCountTxt.text = DyingSankesCount.ToStringBtmFast();
//    }

    //	public int scalablePiecesCount = 5;
    //	public float scaleOffset;

    // Use this for initialization
    void Awake()
    {
		BGSoundManager.Instance.StopPlaying ();
//        DyingSnakesCountTxt.text = "DyingSnakes 0";
        if (forPhotonObj)
            forPhotonObj.SetActive(false);


        //Debug.LogError ("------ Gamemanager slither Awake 111111111");

        //inGameLTString = InGameLenghtText.text;
        //GameOverLTstring = GameOverLenghtText.text;
        instance = this;

		int playedCountForResume=PlayerPrefs.GetInt ("playedCountForResume",1);
		if (playedCountForResume >= 3) {  
			IsEnableWatchVideoToResume = true;
			PlayerPrefs.SetInt ("playedCountForResume", 1);
		} else {
			IsEnableWatchVideoToResume = false;
			playedCountForResume++;
			PlayerPrefs.SetInt ("playedCountForResume", playedCountForResume);
		}

		ShowVideoInResultPage = false;
		IsPauseYesClicked = false;
		IsOpenResultPageCalled = false;
        //		calculate ();

		isTargetReached = false;
		isPlayerBlasted = false;

//		isOfflineMode = true;
//		isForceOfflineMode = false;
//		challengeModeType = 3;

		Debug.Log ("---------"+isForceOfflineMode+":::"+isOfflineMode+":::challengeModetype="+challengeModeType);

		if (challengeModeType != 0) {
			setChallengeType ();
		}
		switch (challengeModeType) {
		case 0:
			Debug.Log ("00000000000");
			isStartGamePlay = true;
			challengeModeInfoPop.transform.parent.gameObject.SetActive (false);
			playTimeDisplay.transform.parent.gameObject.SetActive (false);
			challengeModeInfo.transform.parent.gameObject.SetActive (false);
			break;
		case 1:
			Debug.Log ("11111111111");
			isStartGamePlay = false;
			challengeModeInfoPop.transform.parent.gameObject.SetActive (true);
			playTimeDisplay.transform.parent.gameObject.SetActive (true);
			challengeModeInfo.transform.parent.gameObject.SetActive (true);
			break;
		case 2:
			Debug.Log ("2222222222");
			isStartGamePlay = false;
			challengeModeInfoPop.transform.parent.gameObject.SetActive (true);
			playTimeDisplay.transform.parent.gameObject.SetActive (true);
			challengeModeInfo.transform.parent.gameObject.SetActive (true);
			playTimeDisplay.text = "Size: " + 0;
			break;
		case 3:
			Debug.Log ("3333333333");
			isStartGamePlay = false;
			challengeModeInfoPop.transform.parent.gameObject.SetActive (true);
			playTimeDisplay.transform.parent.gameObject.SetActive (true);
			challengeModeInfo.transform.parent.gameObject.SetActive (true);
			playTimeDisplay.text = "Kills: " + enemiesKilledCount;
			break;
		}
        //Debug.Log ("------ Gamemanager slither Awake 222222");

        onlinePlayerNamesListNew = onlinePlayerNamesList.Split('\n');
        offlinePlayerNamesListNew = offlinePlayerNamesList.Split('\n');

        int offlinePlayerNamesCount = offlinePlayerNamesListNew.Length;
        int forceOfflinePlayerNamesCount = onlinePlayerNamesListNew.Length;

        AIOfflinePlayrNames = new string[offlinePlayerNamesCount];
        for (int i = 0; i < AIOfflinePlayrNames.Length; i++)
        {
            AIOfflinePlayrNames[i] = offlinePlayerNamesListNew[i];
        }

        AIOnlinePlayrNames = new string[forceOfflinePlayerNamesCount];
        for (int i = 0; i < AIOnlinePlayrNames.Length; i++)
        {
            AIOnlinePlayrNames[i] = onlinePlayerNamesListNew[i];
        }

        //Debug.Log ("------ Gamemanager slither Awake 3333333");

        if (isOfflineMode && !isForceOfflineMode)
        {
            //Debug.Log ("------ Gamemanager slither Awake 44444444");

            int AIPlayersCount = Population.instance.big + Population.instance.medium + Population.instance.small;
            //Debug.Log ("------ Gamemanager slither Awake 555555");

            AIPlayerNameIndex = Random.Range(0, (offlinePlayerNamesListNew.Length - AIPlayersCount));
            //Debug.Log ("------ Gamemanager slither Awake 666666");

        }

        if (isOfflineMode && isForceOfflineMode)
        {
            //Debug.Log ("------ Gamemanager slither Awake 44444444");
            int AIPlayersCount = Population.instance.big + Population.instance.medium + Population.instance.small;
            //Debug.Log ("------ Gamemanager slither Awake 555555");
            AIPlayerNameIndex = Random.Range(0, (onlinePlayerNamesListNew.Length - AIPlayersCount - 5));
            //Debug.Log ("------ Gamemanager slither Awake 666666");
        }

		enemiesKilledCount = 0;
		noNetPopObj.SetActive (false);
		LBList.SetActive (false);
        //Debug.Log ("------ Gamemanager slither Awake 7777");

    }
    public GameObject forPhotonObj;

    void Start()
    {

		//Debug.Log ("------ Gamemanager slither start");
		//        loading.SetActive(false);
		//        netSlowPop.SetActive(false);
		//        challengeInfo.SetActive(false);
		//        challengeInfoPlayArea.SetActive(false);
		players = new Dictionary<string, GameObject>();

		AudioClipManager.Instance.Play (InGameSounds.BG);


		currentPlayTime = 0;

		if (challengeModeType != 0) {
			StartCoroutine (showChallengeModePop (0f));

		}
		Invoke ("hideLoading",1f);
		#if Adsetup_ON
		if (AdManager.instance) {
			AdManager.instance.RunActions (AdManager.PageType.InGame,1);
		}
		#endif

		levelNumber++;
		PlayerPrefs.SetInt("playedCount", levelNumber);
		int playedCount = PlayerPrefs.GetInt("playedCountForVideo", 0);
		playedCount++;
		PlayerPrefs.SetInt("playedCountForVideo", playedCount);

		int playedCountForUpgrade = PlayerPrefs.GetInt("playedCountForUpgrade", 0);
		playedCountForUpgrade++;
		PlayerPrefs.SetInt("playedCountForUpgrade", playedCountForUpgrade);

//       	InvokeRepeating("setPlayersLB",3,1);
//		Invoke ("EnalbeLBList",3.1f);
        //Debug.Log ("------ Gamemanager slither start 22222222");
		//if (PlayerPrefs.HasKey ("HelpFirstTime")) {
		//	OnPlayButton ();
		//} else {
		//	Invoke ("OpenControlsPage", 0.5f);
		//}

		//OnPlayButton();
	}
	void OpenControlsPage()
	{
		ControlsPage.mee.open ();
	}
	void EnalbeLBList()
	{
		LBList.SetActive (true);
	}
	void hideLoading()
	{
		loadingImg.SetActive (false);
	}
	IEnumerator showChallengeModePop(float waitTime)
	{
		yield return new WaitForSeconds (0);

		challengeModeInfoPop.transform.localPosition = Vector3.zero;
		iTween.MoveFrom (challengeModeInfoPop, iTween.Hash ("y", -1000, "time", 0.5f, "easetype", iTween.EaseType.spring));

		yield return new WaitForSeconds (2);

		iTween.MoveTo (challengeModeInfoPop, iTween.Hash ("y", -1000, "time", 0.5f, "easetype", iTween.EaseType.easeInBack));

		yield return new WaitForSeconds (0.5f);

		challengeModeInfoPop.transform.parent.gameObject.SetActive (false);
		isStartGamePlay = true;
	}
	void Update()
	{
		if (challengeModeType != 0 && !isTargetReached && !isPlayerBlasted) {
			if (challengeModeType == 1 && isStartGamePlay) {
				currentPlayTime += Time.deltaTime;

				checkTargetTime ();

			}
		}
	}
	public int surviveTime=100,surviveSize=100,enemyKillsTarget=10;
	public void setChallengeType()
	{
		int playerLevel = PlayerPrefs.GetInt ("PlayerLevel",1);
		//		switch(playerLevel)
		//		{
		//		case 1:
		//			surviveTime = 90;
		//			surviveSize = 100;
		//			enemyKillsTarget = 10;
		//			break;
		//		case 2:
		//			surviveTime = 15;
		//			surviveSize = 15;
		//			enemyKillsTarget = 2;
		//			break;
		//		}

		surviveTime = 60+(playerLevel-1)*30;
		surviveSize = 600+(playerLevel-1)*100;
		enemyKillsTarget = 5+(playerLevel-1)*2;

		switch (challengeModeType) {
		case 1:
			string minutes = Mathf.Floor (surviveTime / 60).ToString ("00");
			string seconds = Mathf.Floor (surviveTime % 60).ToString ("00");
			challengeModeInfo.text= "survive for "+(minutes + ":" + seconds) +" sec";
			challengeModeInfoPop.GetComponentInChildren<Text>().text = "survive for "+(minutes + ":" + seconds) +" sec";
			break;
		case 2:
			challengeModeInfo.text = "Grow and reach size of " + surviveSize;
			challengeModeInfoPop.GetComponentInChildren<Text>().text = "Grow and reach size of " + surviveSize;
			break;
		case 3:
			if (enemyKillsTarget == 1) {
				challengeModeInfo.text = "Destroy " + enemyKillsTarget + " king!";
				challengeModeInfoPop.GetComponentInChildren<Text> ().text = "Destroy " + enemyKillsTarget + " king!";
			} else {
				challengeModeInfo.text = "Destroy " + enemyKillsTarget + " kings!";
				challengeModeInfoPop.GetComponentInChildren<Text> ().text = "Destroy " + enemyKillsTarget + " kings!";
			}
			break;
		}
	}
	public void checkTargetTime()
	{
		string minutes = Mathf.Floor (currentPlayTime / 60).ToString ("00");
		string seconds = Mathf.Floor (currentPlayTime % 60).ToString ("00");
		//				timeBar.fillAmount = levelTime / defaultTime;
		playTimeDisplay.text = (minutes + ":" + seconds);

		if (currentPlayTime >= surviveTime) {
			Debug.Log ("---- show Target reached");
			showTargetReached ();
		}
	}
	public void checkTargetSize(int playerMass)
	{
		Debug.Log ("checkTargetSize="+playerMass);
		playTimeDisplay.text="Size: "+playerMass;
		if (playerMass >= surviveSize) {
			Debug.Log ("---- show Target reached");
			myPlayerMass = playerMass;
			showTargetReached ();
		}
	}
	public void checkTargetKills()
	{
		playTimeDisplay.text="Kills: "+enemiesKilledCount;

		if (enemiesKilledCount >= enemyKillsTarget) {
			Debug.Log ("---- show Target reached");
			showTargetReached ();
		}
	}
	public void showTargetReached()
	{
		if (isPlayerBlasted) {
			return;
		}
		isTargetReached = true;
//		for (int i = 0; i < myPlayerList.Count; i++) {
//			myPlayerList [i].GetComponent<CircleCollider2D> ().enabled = false;
//
//		}
//		for (int i = 0; i < AIPlayers.Length; i++) {
//			if (AIPlayers [i] != null) {
//				AIPlayers [i].GetComponent<enemyController> ().removeNearObj ();
//			}
//		}
		if (challengeModeType == 3) {
			//level up
			int playerLevel = PlayerPrefs.GetInt ("PlayerLevel", 1);
			playerLevel++;
			PlayerPrefs.SetInt ("PlayerLevel", playerLevel);
			PlayerPrefs.SetInt ("challengeType",1);

			int prevBestScore = PlayerPrefs.GetInt(StoreManager.Best_Score, 0);
			if (prevBestScore < myPlayerMass)
			{
				PlayerPrefs.SetInt(StoreManager.Best_Score, myPlayerMass);
			}

//			levelNumber++;
//			PlayerPrefs.SetInt("playedCount", levelNumber);
//			int playedCount = PlayerPrefs.GetInt("playedCountForVideo", 0);
//			playedCount++;
//			PlayerPrefs.SetInt("playedCountForVideo", playedCount);
//
//			int playedCountForUpgrade = PlayerPrefs.GetInt("playedCountForUpgrade", 0);
//			playedCountForUpgrade++;
//			PlayerPrefs.SetInt("playedCountForUpgrade", playedCountForUpgrade);

			cameFromPlayArea = true;

			pausePop.SetActive (false);
			playerSnake.SetActive (false);
			LevelUpPop.mee.Open ();

		} else {
			PlayerPrefs.SetInt ("challengeType",(challengeModeType+1));

			int prevBestScore = PlayerPrefs.GetInt(StoreManager.Best_Score, 0);
			if (prevBestScore < myPlayerMass)
			{
				PlayerPrefs.SetInt(StoreManager.Best_Score, myPlayerMass);
			}

//			levelNumber++;
//			PlayerPrefs.SetInt("playedCount", levelNumber);
//			int playedCount = PlayerPrefs.GetInt("playedCountForVideo", 0);
//			playedCount++;
//			PlayerPrefs.SetInt("playedCountForVideo", playedCount);
//
//			int playedCountForUpgrade = PlayerPrefs.GetInt("playedCountForUpgrade", 0);
//			playedCountForUpgrade++;
//			PlayerPrefs.SetInt("playedCountForUpgrade", playedCountForUpgrade);

			cameFromPlayArea = true;

			pausePop.SetActive (false);
			playerSnake.SetActive (false);
			TargetCompletePop.mee.Open ();
		}
		Debug.Log ("-------- show target reached");

	}
    //	float Initmoney=50000;
    //	float moneyValue=50000;
    //	float intereset=6;
    //	float finalMoney;
    //	void calculate()
    //	{
    //		for (int i = 0; i < 5; i++) {
    //			moneyValue += (moneyValue * intereset / 100);
    //			finalMoney = moneyValue;
    //			Debug.Log ("moneyvalue after year="+(i+1)+"::is="+moneyValue);
    //			moneyValue += Initmoney;
    //		}
    //
    //		for (int i = 0; i < 5; i++) {
    //			finalMoney += (finalMoney * intereset / 100);
    //			Debug.Log ("moneyvalue after year="+(i+1)+"::is="+finalMoney);
    ////			moneyValue += Initmoney;
    //		}
    //	}

    public void OnGameOver_Event()
    {
        //        AdNetworks.instance.ShowInterstitial();
//        InGameJoystickCanvas.SetActive(false);
    }


	public IDictionary<string, GameObject> players;

	public void OnPlayButton(IMatch match)
    {
		Debug.Log("------ OnPlayButton count="+match.Size);
//        InGameJoystickCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
		//foreach (var user in match.Presences)
		//{
		//	SpawnPlayer(match.Id, user);
		//}
		//SpawnPlayer();

		//playerSnake.SetActive(true);
//        Snake MysnakeComponent = playerSnake.GetComponent<Snake>();
//        MysnakeComponent.GetARandomTemplate();
//        playerSnake.GetComponent<Snake>().ShowPlayerName();
//        CameraManager.setMySnakeAsPlayer(MysnakeComponent, MysnakeComponent.SnakeHead.gameObject);
//		//Population.instance.StartPopulation ();
//		Population.instance.SpawnSnake(200, true);
//		//InvokeRepeating("setPlayersLB",3,1);
//		//Invoke ("EnalbeLBList",3.1f);
////		Invoke ("dummyFail",3);
//		snakeHeadObj = playerSnake.GetComponent<Snake> ().SnakeHead.gameObject;
//		body = snakeHeadObj.GetComponent<Rigidbody> ();
    }
	public IUserPresence localUser;

	public void SpawnPlayer(string matchId, IUserPresence user, int spawnIndex = -1)
	{
		Debug.LogError("------ SpawnPlayer 111111");
		if (players.ContainsKey(user.SessionId))
		{
			return;
		}

		var isLocal = user.SessionId == localUser.SessionId;

		bool IsMyPlayer = false;
		IsMyPlayer = isLocal ? true : false;
		Debug.LogError("------ SpawnPlayer 222222 IsMyPlayer="+IsMyPlayer);

		GameObject PlayerObj =Population.instance.SpawnSnake(200,IsMyPlayer);
		// Choose the appropriate player prefab based on if it's the local player or not.
		//var playerPrefab = isLocal ? NetworkLocalPlayerPrefab : NetworkRemotePlayerPrefab;

		// Spawn the new player.
		//var player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);



		// Setup the appropriate network data values if this is a remote player.
		if (!isLocal)
		{
			Debug.LogError("------ SpawnPlayer not islocal");

			//player.GetComponent<PlayerNetworkRemoteSync>().NetworkData = new RemotePlayerNetworkData
			//{
			//	MatchId = matchId,
			//	User = user
			//};
		}
		else
        {
			Debug.LogError("------ SpawnPlayer local");

			playerSnake = PlayerObj;
			Snake MysnakeComponent = playerSnake.GetComponent<Snake>();
			MysnakeComponent.GetARandomTemplate();
			playerSnake.GetComponent<Snake>().ShowPlayerName();
			CameraManager.setMySnakeAsPlayer(MysnakeComponent, MysnakeComponent.SnakeHead.gameObject);
			//Population.instance.StartPopulation ();
			//Population.instance.SpawnSnake(200, true);
			//InvokeRepeating("setPlayersLB",3,1);
			//Invoke ("EnalbeLBList",3.1f);
			//		Invoke ("dummyFail",3);
			snakeHeadObj = playerSnake.GetComponent<Snake>().SnakeHead.gameObject;
			body = snakeHeadObj.GetComponent<Rigidbody>();
		}

		// Add the player to the players array.
		players.Add(user.SessionId, PlayerObj);
	}

	public void dummyFail()
	{
		playerSnake.GetComponent<Snake> ().DeathRoutine();
	}
    public void OnGameOverPlayButton()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void pauseClick()
    {
		AudioClipManager.Instance.Play (InGameSounds.Button);
        //		Time.timeScale = 0;
        pausePop.SetActive(true);
    }
    public void pauseYesClick()
    {
		AudioClipManager.Instance.Play (InGameSounds.Button);
        Time.timeScale = 1;
        pausePop.SetActive(false);
		IsPauseYesClicked = true;

        Time.timeScale = 1;
        openResultPage();
        //		Application.LoadLevel ("Menu");
    }
    public void pauseNoClick()
    {
		AudioClipManager.Instance.Play (InGameSounds.Button);
        pausePop.SetActive(false);
        Time.timeScale = 1;
    }

	bool IsOpenResultPageCalled=false;

    public void openResultPage()
    {
		if (IsOpenResultPageCalled)
			return;
		Population.instance.resetPopulationOnDie();
        int prevBestScore = PlayerPrefs.GetInt(StoreManager.Best_Score, 0);
        if (prevBestScore < playerSnake.GetComponent<Snake>().getPoints())
        {
            PlayerPrefs.SetInt(StoreManager.Best_Score, playerSnake.GetComponent<Snake>().getPoints());
        }

//        levelNumber++;
//        PlayerPrefs.SetInt("playedCount", levelNumber);
//        int playedCount = PlayerPrefs.GetInt("playedCountForVideo", 0);
//        playedCount++;
//        PlayerPrefs.SetInt("playedCountForVideo", playedCount);
//
//        int playedCountForUpgrade = PlayerPrefs.GetInt("playedCountForUpgrade", 0);
//        playedCountForUpgrade++;
//        PlayerPrefs.SetInt("playedCountForUpgrade", playedCountForUpgrade);

		isPlayerBlasted = true;
        cameFromPlayArea = true;
        pausePop.SetActive(false);
		IsOpenResultPageCalled = true;
        ResultPage.mee.Open();
        //		Application.LoadLevel ("Menu");
    }

    public void setPlayersLB()
    {
        //				Debug.LogError ("----- setPlayersLB");
        for (int i = 0; i < playersListLB.Count; i++)
        {
            for (int j = i + 1; j < playersListLB.Count; j++)
            {
                float tempPoint1 = 0;
                float tempPoint2 = 0;
                GameObject tempObj = null;
                //				if(playersListLB[i].tag=="Player")
                //				{
                tempPoint1 = playersListLB[i].GetComponent<Snake>().getPoints();
                tempPoint2 = playersListLB[j].GetComponent<Snake>().getPoints();
                //				}
                //				else if(playersListLB[i].tag=="Enemy")
                //				{
                //					tempMass1=playersListLB[i].GetComponent<Snake>().points;
                //				}
                //				if(playersListLB[j].tag=="Player")
                //				{
                //					tempMass2=playersListLB[j].GetComponent<Snake>().points;
                //				}
                //				else if(playersListLB[j].tag=="Enemy")
                //				{
                //					tempMass2=playersListLB[j].GetComponent<Snake>().points;
                //				}

                if (tempPoint2 > tempPoint1)
                {
                    tempObj = playersListLB[i];
                    playersListLB[i] = playersListLB[j];
                    playersListLB[j] = tempObj;
                }
            }
        }
		for (int i = 0; i < playersListLB.Count; i++) {
			if (playersListLB [i].GetComponent<Snake> ().points == myScore) {
				myPlayerRank = (i + 1);
				break;
			}
		}

        for (int i = 0; i < playersNameListLB.Count-1; i++)
        {
            if (i < playersListLB.Count)
            {
                playersNameListLB[i].SetActive(true);
                //				if (playersListLB [i].tag == "Player") {
                playersNameListLB[i].GetComponent<Text>().text = playersListLB[i].GetComponent<Snake>().playerName;
                playersNameListLB[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = playersListLB[i].GetComponent<Snake>().getPoints() + "";
                //				} else if (playersListLB [i].tag == "Enemy") {
                //					playersNameListLB [i].GetComponent<Text> ().text = playersListLB [i].GetComponent<enemyController> ().playerName;
                //					playersNameListLB [i].transform.GetChild (0).gameObject.GetComponent<Text> ().text = playersListLB [i].GetComponent<Snake> ().points + "";
                //				}
            }
            else
            {
				playersNameListLB[i].gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
		if (myPlayerRank > 5) {
			if (!myPlayerRankTxt.gameObject.transform.parent.gameObject.activeInHierarchy) {
				for (int i = 0; i < playersLBGlow.Count; i++) {
					playersLBGlow [i].SetActive (false);
				}
				myPlayerRankTxt.gameObject.transform.parent.gameObject.SetActive (true);
				playersNameListLB [playersNameListLB.Count - 1].SetActive (true);
				playersLBGlow [playersLBGlow.Count - 1].SetActive (true);
			}
			//				if (playersListLB [i].tag == "Player") {
			playersNameListLB [playersNameListLB.Count - 1].GetComponent<Text> ().text = myPlayerName;
			playersNameListLB [playersNameListLB.Count - 1].transform.GetChild (0).gameObject.GetComponent<Text> ().text = (myScore-200) + "";
			myPlayerRankTxt.text = myPlayerRank + "";
		} else {				
				myPlayerRankTxt.gameObject.transform.parent.gameObject.SetActive (false);
				for (int i = 0; i < playersLBGlow.Count; i++) {
					playersLBGlow [i].SetActive (false);
				}
				playersLBGlow [myPlayerRank - 1].SetActive (true);
		}
        //btm2018
        /*
		if (isOfflineMode ==true && isForceOfflineMode == true && GirlGameConfigs.mee!=null && !GirlGameConfigs.mee.isWifi_OR_Data_Availble ()) {
			timeOutNGoBackToMenu();
		}*/

    }
    public void timeOutNGoBackToMenu()
    {
        loadingImg.SetActive(false);
        noNetPopObj.SetActive(true);
        noNetPopObj.transform.localPosition = Vector3.zero;
        iTween.MoveFrom(noNetPop, iTween.Hash("y", -1000, "time", 0.5f, "islocal", true, "easetype", iTween.EaseType.spring));
        Invoke("loadMenu", 2);
    }
    void loadMenu()
    {
        Application.LoadLevel("Menu");
    }
	public bool IsPauseYesClicked;

	public void ResetGame()
	{
		IsPauseYesClicked = false;
		IsOpenResultPageCalled = false;
		ShowVideoInResultPage = true;
//		isPlayerBlasted = false;
		generatePos ();

//		isEnableCollisions = false;
//		playerSnake.SetActive (true);
//		playerSnake.GetComponent<Snake> ().dieing = false;
//
//		GameManagerSlither.instance.isEnableCollisions = true;
//		isPlayerBlasted = false;
		//		BGSoundManager.Instance.pla
	}
	public Rigidbody testBody;
	GameObject snakeHeadObj;
	Rigidbody body;
	public void destroyPlayer()
	{
		Debug.Log ("-------------- DestroyPlayer");
//		isPlayerBlasted = true;
//		playerSnake.transform.SetParent (myPlayerParent.transform);
//		myPlayerParent.GetComponent<Animation> ().Play ();
//		return;

		isPlayerBlasted = true;
		body.useGravity = true;
		snakeHeadObj.transform.localPosition = new Vector3 (snakeHeadObj.transform.localPosition.x, snakeHeadObj.transform.localPosition.y + 3, snakeHeadObj.transform.localPosition.z);
		body.AddForce(Random.Range(-5,5), 0, 1000f, ForceMode.Acceleration);

		float scaleValue = snakeHeadObj.transform.localScale.x;
		iTween.ScaleTo (snakeHeadObj,iTween.Hash("x",(scaleValue+0.5f),"z",(scaleValue+0.5f),"time",0.4f,"easetype",iTween.EaseType.linear));
		iTween.ScaleTo (snakeHeadObj,iTween.Hash("x",(scaleValue),"z",(scaleValue),"time",1f,"delay",0.4f,"easetype",iTween.EaseType.linear));

		iTween.RotateBy (snakeHeadObj,iTween.Hash("y",10f,"time",10f,"easetype",iTween.EaseType.linear));


//		Invoke ("ResetGame",5);
//		testBody.useGravity = true;
		//		playerToDestory.transform.localPosition = playerToDestory.transform.localPosition+new Vector3(0,0,-1);
		//		body.constraints.
//		body.constraints = RigidbodyConstraints.FreezePositionY;
//		testBody.AddForce(transform.forward * 100f);
//		testBody.AddForce(0, 0f, 500f, ForceMode.Acceleration);
//		testBody.AddExplosionForce (100, testBody.gameObject.transform.position-new Vector3(0,0,-5), 10);
	}
	public LayerMask otherPlayerMask;
	int repeatCount=0;

	void generatePos()
	{
		GameObject myPlayer = null;

		Vector3 myPos = new Vector3 ( Random.Range(-Population.instance.RangeX , Population.instance.RangeX )  ,0 , Random.Range(-Population.instance.RangeZ , Population.instance.RangeZ ) )  ;

		Collider2D[] Collider = Physics2D.OverlapBoxAll (myPos, new Vector3 (20, 20, 0), 0, otherPlayerMask);
		if (Collider.Length != 0 && repeatCount<100) {
			//			Debug.LogError ("-------------GeneratePos recheck again");
			repeatCount++;
			generatePos ();
		} else {
			iTween.Stop (snakeHeadObj);
			isPlayerBlasted = false;
			body.useGravity = false;
			body.isKinematic = true;
			body.velocity = Vector3.zero;
			playerSnake.SetActive (true);
			playerSnake.transform.localPosition = myPos;
			snakeHeadObj.transform.localPosition = Vector3.zero;
			playerSnake.GetComponent<Snake> ().dieing = false;

			playerSnake.GetComponent<Snake> ().strength=100;
				float scaleX = 1;
			playerSnake.GetComponent<Snake> ().healthFillBar.transform.localScale = new Vector3 (scaleX, 1, 4);
			body.isKinematic = false;

//			InGameJoystickCanvas.SetActive(true);
//				showCountDownTimerAnim ();
		}

	}
}
