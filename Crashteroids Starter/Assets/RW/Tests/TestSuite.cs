using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Specialized;

public class TestSuite
{
    private Game game;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game.gameObject);
    }

    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        float initialYPos = asteroid.transform.position.y;
        yield return new WaitForSeconds(0.1f);

        Assert.Less(asteroid.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = game.GetShip().transform.position;
        yield return new WaitForSeconds(0.1f);

        Assert.True(game.isGameOver);
    }

    [UnityTest]
    public IEnumerator NewGameRestartsGame()
    {
        game.isGameOver = true;
        game.NewGame();

        Assert.False(game.isGameOver);
        yield return null;
    }

    [UnityTest]
    public IEnumerator LaserMovesUp()
    {

        GameObject laser = game.GetShip().SpawnLaser();

        float initialYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);

        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator DestroyedAsteroidRaisesScore()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        Assert.AreEqual(game.score, 1);
    }

    [UnityTest]
    public IEnumerator CheckGameScoreIsEqualToZero()
    {
        Assert.AreEqual(game.score, 0);

        game.score = 2;

        game.isGameOver = true;
        game.NewGame();

        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(game.score, 0);
    }


    [UnityTest]
    public IEnumerator ShipMovesLeft()
    {
        GameObject ship = game.GetShip().gameObject;

        float initialXPos = ship.transform.position.x;
        yield return new WaitForSeconds(0.1f);
        game.GetShip().MoveLeft();
        Assert.Less(ship.transform.position.x, initialXPos);
    }

    [UnityTest]
    public IEnumerator ShipMovesRight()
    {
        GameObject ship = game.GetShip().gameObject;

        float initialXPos = ship.transform.position.x;
        yield return new WaitForSeconds(0.1f);
        game.GetShip().MoveRight();
        Assert.Greater(ship.transform.position.x, initialXPos);
    }

    [UnityTest]
    public IEnumerator ShieldIsToggled()
    {
        Shield shield = game.GetShield();

        shield.isToggled = false;

        shield.ToggleShield();

        yield return new WaitForSeconds(0.1f);

        Assert.True(shield.isToggled);
    }

    [UnityTest]
    public IEnumerator ShieldDestroyedOnCollision()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero + Vector3.up;
        Shield shield = game.GetShield();
        shield.transform.position = Vector3.zero;

        yield return new WaitForSeconds(0.5f);

        Assert.True(shield.isDestroyed);
    }

    [UnityTest]
    public IEnumerator ShieldRepairedOnNewGame()
    {
        Shield shield = game.GetShield();

        game.isGameOver = true;

        shield.DestroyShield();

        Assert.True(shield.isDestroyed);

        yield return new WaitForSeconds(0.1f);

        game.NewGame();

        Assert.False(shield.isDestroyed);
        Assert.False(shield.isToggled);
    }
    public IEnumerator ShipMovesUp()
    {
        GameObject ship = game.GetShip().gameObject;

        float initialYPos = ship.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        game.GetShip().MoveUp();
        Assert.Greater(ship.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator ShipMovesDown()
    {
        GameObject ship = game.GetShip().gameObject;

        float initialYPos = ship.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        game.GetShip().MoveDown();
        Assert.Greater(ship.transform.position.y, initialYPos);
    }
}
