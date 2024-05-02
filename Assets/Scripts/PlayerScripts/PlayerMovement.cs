using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I hate this code... was much nicer before but has been changed for assignment

public interface IPlayerState {
    public IPlayerState Tick(GameObject player, PlayerInput input);
    public void Enter(GameObject player, float speed);
    public void Exit(GameObject player);
}

public class PlayerInput {
    public bool W, A, S, D;

    public PlayerInput(bool W, bool A, bool S, bool D) {
        this.W = W;
        this.A = A;
        this.S = S;
        this.D = D;
    } 
}

public class PlayerIdle : IPlayerState {
    public void Enter(GameObject player, float speed) {
        Debug.Log("I am idle");
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public IPlayerState Tick(GameObject player, PlayerInput input) {
        if (input.A && (input.W || input.S)) return new PlayerLDiag();
        if (input.D && (input.W || input.S)) return new PlayerRDiag();
        if (input.W || input.S) return new PlayerUpDown();
        if (input.A) return new PlayerLeft();
        if (input.D) return new PlayerRight();
        
        return null; //stay idle
    }

    public void Exit(GameObject player) {
        
    }
}

public class PlayerLeft : IPlayerState {
    public void Enter(GameObject player, float speed) {
        Debug.Log("I am left");
        player.GetComponent<Animator>().SetBool("leftGlide", true);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2( -speed, 0);


    }

    public IPlayerState Tick(GameObject player, PlayerInput input) {
        if (input.A && (input.W || input.S)) return new PlayerLDiag();
        if (input.D && (input.W || input.S)) return new PlayerRDiag();
        if (input.W || input.S) return new PlayerUpDown();
        if (input.A) return null;
        if (input.D) return new PlayerRight();

        return new PlayerIdle(); 
    }

    public void Exit(GameObject player) {
        player.GetComponent<Animator>().SetBool("leftGlide", false);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}

public class PlayerRight : IPlayerState {
    public void Enter(GameObject player, float speed) {
        Debug.Log("I am right");
        player.GetComponent<Animator>().SetBool("rightGlide", true);        
        player.GetComponent<Rigidbody2D>().velocity = new Vector2( speed, 0);

    }

    public IPlayerState Tick(GameObject player, PlayerInput input) {
        if (input.A && (input.W || input.S)) return new PlayerLDiag();
        if (input.D && (input.W || input.S)) return new PlayerRDiag();
        if (input.W || input.S) return new PlayerUpDown();
        if (input.A) return new PlayerLeft();
        if (input.D) return null;

        return new PlayerIdle(); 
    }

    public void Exit(GameObject player) {
        player.GetComponent<Animator>().SetBool("rightGlide", false);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}

public class PlayerUpDown : IPlayerState {
    public void Enter(GameObject player, float speed) {
        Debug.Log("I am upDown");
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Input.GetAxisRaw("Vertical") * speed);

    }

    public IPlayerState Tick(GameObject player, PlayerInput input) {
        if (input.A && (input.W || input.S)) return new PlayerLDiag();
        if (input.D && (input.W || input.S)) return new PlayerRDiag();
        if (input.W || input.S) return null;
        if (input.A) return new PlayerLeft();
        if (input.D) return new PlayerRight();
        
        return new PlayerIdle(); 
    }

    public void Exit(GameObject player) {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}

public class PlayerLDiag : IPlayerState {
    public void Enter(GameObject player, float speed) {
        Debug.Log("I am diag");
        float speedLimit = speed * 0.7f;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2( -speedLimit, Input.GetAxisRaw("Vertical") * speedLimit);
    }

    public IPlayerState Tick(GameObject player, PlayerInput input) {
        if (input.A && (input.W || input.S)) return null;
        if (input.D && (input.W || input.S)) return new PlayerRDiag();
        if (input.W || input.S) return new PlayerUpDown();
        if (input.A) return new PlayerLeft();
        if (input.D) return new PlayerRight();
        
        return new PlayerIdle(); 
    }

    public void Exit(GameObject player) {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}

public class PlayerRDiag : IPlayerState {
    public void Enter(GameObject player, float speed) {
        Debug.Log("I am diag");
        float speedLimit = speed * 0.7f;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2( speedLimit, Input.GetAxisRaw("Vertical") * speedLimit);
    }

    public IPlayerState Tick(GameObject player, PlayerInput input) {
        if (input.A && (input.W || input.S)) return new PlayerLDiag();
        if (input.D && (input.W || input.S)) return null;
        if (input.W || input.S) return new PlayerUpDown();
        if (input.A) return new PlayerLeft();
        if (input.D) return new PlayerRight();
        
        return new PlayerIdle(); 
    }

    public void Exit(GameObject player) {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}

public class PlayerMovement : MonoBehaviour {
    private Vector2 START_POS = new Vector2(0f, -4.5f);
    private IPlayerState currentState = new PlayerIdle();

    private float speed = 4f;
    
    private void Start() {
        currentState.Enter(this.gameObject, speed);
    }
    
    private void SetSpeed(float val) {
        speed = val;
    }

    private void Update() {
        UpdateState(new PlayerInput(
            Input.GetKey(KeyCode.W), 
            Input.GetKey(KeyCode.A), 
            Input.GetKey(KeyCode.S), 
            Input.GetKey(KeyCode.D)
            ));
    }

    private void UpdateState(PlayerInput input) {
        IPlayerState newState = currentState.Tick(this.gameObject, input);

        if (newState != null) {
            currentState.Exit(this.gameObject);
            currentState = newState;
            newState.Enter(this.gameObject, speed);
        }
    }

    private void ResetLoc() {
        transform.position = START_POS;
    }
}
