using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class K_Means : MonoBehaviour
{
    [Header("Variables")]
    public bool nextPass = false;
    public bool finish = false;
    public bool first_step = true;
    public int steps = 1;

    [Header("Manual Config")]
    public GameObject prefab;
    public GameObject objects;
    public List<Object> init_centroids = null;
    public Button next_interaction_btn;
    public TMP_Text interatcion_txt;
    public GameObject content_prefab;
    public GameObject content;


    [Header("Auto Config")]
    public List<Object> objs = null;
    public List<Centroid> centroids = null;

    public List<string> bests_centroids = null;
    public List<string> last_group = null;
    public List<string> current_group = null;


    void Awake()
    {
        Init_Interaction_Btn();
        Get_Objects();
        Create_Centroids();
    }

    private void Init_Interaction_Btn()
    {
        // Ao clicar no botao o nextPass troca de valor
        next_interaction_btn.onClick.AddListener(() => { nextPass = !nextPass; });
    }

    private void Get_Objects()
    {
        int count = objects.transform.childCount;

        for (int i = 0; i < count; i++)
        {
            Object obj = objects.transform.GetChild(i).GetComponent<Object>();
            objs.Add(obj);
        }
    }

    private void Create_Centroids()
    {

        for (int i = 0; i < init_centroids.Count; i++)
        {
            GameObject new_centroid = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            new_centroid.name = new_centroid.name + " " + (i + 1);

            Centroid centroid = new_centroid.GetComponent<Centroid>();

            Vector2 obj_pos = init_centroids[i].transform.position;

            Vector2 coords = new(obj_pos.x, obj_pos.y);
            Debug.Log("Coords from: " + init_centroids[i] + " " + obj_pos);
            Debug.Log("Coords: " + coords);

            centroid.Set_Coords(coords);

            centroid.Set_Centroid_Color(GetComponent<Main_Colors>().Get_Available_Color());

            centroids.Add(centroid);
        }

    }



    void Update()
    {
        // Se for falso entao nao foi achada a solução
        // Logo continua
        if (finish == false && nextPass == true)
        {
            interatcion_txt.text = steps.ToString();
            // Debug.Log("Step: " + steps);
            Calculate_Distance_To_Centroid(); // 1 e 2

            if (Check_Centroid_Group())
            {
                finish = true;
                return;
            } // 3

            Centroid_Media(); // 4
            steps++;
            first_step = false;
            nextPass = false;
        }


    }


    // 1 - Calcula a distancia entre todos os pontos ate o centroid
    private void Calculate_Distance_To_Centroid()
    {


        bests_centroids = new();

        foreach (Object obj in objs)
        {
            double best_dist = -1;
            Centroid best_centroid = null;


            foreach (Centroid center in centroids)
            {

                double dist_dentroid = 0;
                Vector2 obj_pos = obj.transform.position;
                dist_dentroid = Math.Sqrt(Math.Pow(center.coords.x - obj_pos.x, 2) + Math.Pow(center.coords.y - obj_pos.y, 2));

                // Primeira passagem que seta a distancia do melhor centroid 
                if (best_dist == -1 && best_centroid == null)
                {
                    best_dist = dist_dentroid;
                    best_centroid = center;
                }
                // Verfica se a melhor distancia foi melhorada
                // Se sim entao troca o melhor centroid e melhor distancia
                else if (best_dist > dist_dentroid)
                {
                    best_dist = dist_dentroid;
                    best_centroid = center;

                }
            }

            // 2 - Associa a cada ponto o centroid mais perto  
            // Da ao objeto o melhor centroid escolhido
            obj.Set_Centroid(best_centroid);
            obj.dist_to_centroid = best_dist;

            bests_centroids.Add(best_centroid.gameObject.name);

        }

        // Se for a primeira passagem
        // Adiciona em current
        if (first_step)
        {
            current_group = bests_centroids;
        }
        // Se nao for a primeira passagem
        // Associa current para last 
        // E associa o atual para current
        else
        {
            last_group = new();
            last_group = current_group;

            current_group = new();
            current_group = bests_centroids;

        }

        Set_Group_On_Content(current_group);
    }

    private void Set_Group_On_Content(List<string> this_current_group)
    {
        void Reset_List()
        {
            if (content.transform.childCount > 0)
            {
                for (int i = 0; i < content.transform.childCount; i++)
                {
                    Destroy(content.transform.GetChild(i).gameObject);
                }
            }
        }

        Color Get_Centroid_color(string group)
        {
            GameObject centroid = GameObject.Find(group);
            Color color = centroid.GetComponent<Centroid>().Get_Centroid_Color();
            return color;
        }

        Reset_List();

        foreach (string group in this_current_group)
        {
            GameObject prefab = Instantiate(content_prefab);
            prefab.transform.SetParent(content.transform);

            Best_Choice best_Choice = prefab.GetComponent<Best_Choice>();

            best_Choice.Set_Text(group);
            best_Choice.Set_Text_Color(Get_Centroid_color(group));
        }
    }


    private bool Check_Centroid_Group()
    {
        // Se o last_group estiver vazio entao retorna
        // Pois é a primeira rodada
        if (last_group == null || last_group.Count == 0)
        {
            return false;
        }

        int objs_count = objs.Count;
        int count = 0;

        for (int i = 0; i < objs_count; i++)
        {
            if (last_group[i] == current_group[i])
            {
                count++;
            }
        }

        // Debug.Log("count: " + count);

        if (count == objs_count) return true;
        else return false;


    }


    // 4 - Calcula a media das distancias de cada centroid
    private void Centroid_Media()
    {

        foreach (Centroid center in centroids)
        {
            Debug.Log("==================");
            Debug.Log("Centroid:" + center.gameObject.name);
            float media_x = 0;
            float media_y = 0;
            float count = 0;

            foreach (Object obj in objs)
            {
                // Debug.Log("Obj:" + obj.gameObject.name);
                if (center == obj.centroid)
                {
                    Vector2 obj_pos = obj.transform.position;
                    media_x += obj_pos.x;
                    media_y += obj_pos.y;
                    count++;
                }
            }

            Debug.Log("media_x: " + media_x);
            Debug.Log("media_y: " + media_y);
            Debug.Log("Count: " + count);

            Vector2 new_coords = new((float)(media_x / count), (float)(media_y / count));

            if (count == 0)
            {
                new_coords = new Vector2(0, 0);
            }

            Debug.Log("new_coords: " + new_coords);
            center.Set_Coords(new_coords);
        }
    }



}