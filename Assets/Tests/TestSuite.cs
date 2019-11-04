using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NUnit.Framework;
using UnityEngine.TestTools;

public class TestSuite
{
    private GameObject game;
    private GameManager gameManager;
    private Player player;
    [SetUp]
    public void SetUp()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Game");
        game = Object.Instantiate(prefab);
        gameManager = game.GetComponent<GameManager>();
        //player = game.GetComponent<Player>();
        player = game.GetComponentInChildren<Player>();
    }

    [UnityTest]
    public IEnumerator GamePrefabLoaded()
    {
        yield return new WaitForEndOfFrame();
    }

    [UnityTest]
    public IEnumerator ItemCollidesWithPlayer()
    {
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Entities/Item");
        Vector3 playerPosition = player.transform.position;
        GameObject item = Object.Instantiate(itemPrefab, playerPosition, Quaternion.identity);

        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();

        Assert.IsTrue(item == null);
    }

    [UnityTest]
    public IEnumerator PlayerExists()
    {
        yield return new WaitForEndOfFrame();
        Assert.NotNull(player, "no player");
    }

    [UnityTest]
    public IEnumerator ItemCollectedAndScoreAdded()
    {
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Entities/Item");
        Vector3 playerPosition = player.transform.position;
        GameObject item = Object.Instantiate(itemPrefab, playerPosition, Quaternion.identity);
        int oldScore = gameManager.score;
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();
        int newScore = gameManager.score;
        Assert.IsTrue(oldScore != newScore);
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(gameManager.gameObject);
    }
}
