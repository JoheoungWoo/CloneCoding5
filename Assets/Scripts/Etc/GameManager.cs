using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // 인스턴스가 이미 존재하는지 확인
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 인스턴스 제거
            return;
        }

        Instance = this; // 인스턴스 설정
        DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않도록 설정


        fishTable = new Dictionary<FishType, GameObject>();
    }

    [Header(" - DeadLine")]
    [SerializeField]
    private BoxCollider deadLineCollider;
    private string deadLineName = "DeadLine";

    [Header(" - Game")]
    [SerializeField]
    private bool isGameStart;
    public bool IsGameStart => isGameStart;

    public Fish[] fishPrefabs;
    public Dictionary<FishType, GameObject> fishTable;

    private void Start()
    {
        Start_SetDeadLine();
        Start_SetFishTable();
    }

    private void Start_SetDeadLine()
    {
        deadLineCollider = transform.parent.FindChildByName(deadLineName).GetComponent<BoxCollider>();
        Debug.Assert(deadLineCollider != null);

        // 화면 너비와 높이를 가져옴
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 화면 비율 계산
        float aspectRatio = screenWidth / screenHeight;

        // 픽셀 값을 월드 좌표로 변환 (카메라의 뷰포트 비율에 따라)
        float worldHeight = Camera.main.orthographicSize * 2;
        float worldWidth = worldHeight * aspectRatio; // 비율에 따라 너비 계산

        // BoxCollider의 크기 조정
        deadLineCollider.size = new Vector3(worldWidth, worldHeight, 1);
    }

    private void Start_SetFishTable()
    {
        for(int i = 0; i <  fishPrefabs.Length; i++)
        {
            if (!fishTable.ContainsKey(fishPrefabs[i].type))
                fishTable.Add(fishPrefabs[i].type, fishPrefabs[i].gameObject);
        }

    }

    private void Update()
    {
        if (isGameStart)
            return;

        if (deadLineCollider != null)
        {
            GameStart();
            return;
        }
    }

    public void GameStart()
    {
        isGameStart = true;

        float radius = 50f; // 원의 반지름
        int numPoints = 13; // 생성할 점의 개수

        // 원을 형성하는 점을 생성
        Vector3 center = deadLineCollider.transform.position;
        for (int i = 0; i < numPoints; i++)
        {
            float angle = i * Mathf.PI * 2 / numPoints;
            Vector3 point = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);

            CreateFish(FishType.FishV3,point,transform);
        }
    }

    public void GameOver()
    {
        Debug.Log("게임 오버");
        isGameStart = false;
    }

    private GameObject CreateFish(FishType fishType, Vector3 position, Transform transform)
    {
        if (fishTable.TryGetValue(fishType, out GameObject originalFish))
        {
            GameObject obj = Instantiate(originalFish);
            obj.transform.position = position;
            obj.transform.LookAt(position);
            obj.transform.SetParent(transform);
            return obj;
        }

        Debug.LogWarning($"Fish type {fishType} not found in fishTable.");
        return null;
    }




    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
            return;

        // 콜라이더 범위 확인
        Gizmos.color = Color.green;
        Gizmos.DrawCube(deadLineCollider.transform.position, deadLineCollider.size);

        // 원을 그리기 위한 설정
        Gizmos.color = Color.blue; // 점의 색상 설정
        float radius = 30f; // 원의 반지름
        int numPoints = 36; // 생성할 점의 개수

        // 원을 형성하는 점을 생성
        Vector3 center = deadLineCollider.transform.position;
        for (int i = 0; i < numPoints; i++)
        {
            float angle = i * Mathf.PI * 2 / numPoints;
            Vector3 point = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius,  0);

            Gizmos.DrawSphere(point, 0.5f);
        }
    }

}

