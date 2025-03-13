using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class DialogListener : MonoBehaviour
{
    public string dbPath = "URI=file:" + Path.Combine(Application.dataPath, "Database", "db.db");
    public GameObject watTyler;
    public SpriteRenderer spriteRenderer;
    public Sprite soldierSprite; // Soldier sprite
    public Animator animator;
    public RuntimeAnimatorController soldierController; // Soldier's animations

    public void CheckSQLite()
    {
        int amount = GetLastAmount();
        if (amount < 12)
        {
            // Change sprite to soldier
            spriteRenderer.sprite = soldierSprite;

            // Change animations to soldier's set
            animator.runtimeAnimatorController = soldierController;

            spriteRenderer.transform.localScale = new Vector3(1f, 1f, 1f);

            CapsuleCollider2D collider = watTyler.GetComponent<CapsuleCollider2D>();
            if (collider != null)
            {
                collider.offset = new Vector2(-0.003986359f, -0.3290749f);
                collider.size = new Vector2(1.810539f, 2.921774f);
            }
        }
    }

    int GetLastAmount()
    {
        int amount = 0;
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT monto FROM DIALOGUES ORDER BY id DESC LIMIT 1";
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        amount = reader.GetInt32(0);
                    }
                }
            }
        }
        return amount;
    }

}
