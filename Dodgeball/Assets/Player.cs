using UnityEngine;

/// <summary>
/// Control the player on screen
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// Prefab for the orbs we will shoot
    /// </summary>
    public GameObject OrbPrefab;

    /// <summary>
    /// How fast our engines can accelerate us
    /// </summary>
    public float EnginePower = 1;
    
    /// <summary>
    /// How fast we turn in place
    /// </summary>
    public float RotateSpeed = 1;

    /// <summary>
    /// How fast we should shoot our orbs
    /// </summary>
    public float OrbVelocity = 10;

    /// <summary>
    /// Our rigid body component
    /// Used to apply forces so we can move around
    /// </summary>
    private Rigidbody2D rigidBody;

    /// <summary>
    /// Initialize rigidBody field
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Handle moving and firing.
    /// Called by Unity every 1/50th of a second, regardless of the graphics card's frame rate
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void FixedUpdate()
    {
        Manoeuvre();
        MaybeFire();
    }

    /// <summary>
    /// Fire if the player is pushing the button for the Fire axis
    /// Unlike the Enemies, the player has no cooldown, so they shoot a whole blob of orbs
    /// </summary>
    void MaybeFire()
    {
        // TODO
        var fireAxis = Input.GetAxis("Fire");
        if (fireAxis == 1)
        {
            for (int i = 0; i < 10; i++)
            {
                FireOrb();
            }
        }
    }

    /// <summary>
    /// Fire one orb.  The orb should be placed one unit "in front" of the player.
    /// transform.right will give us a vector in the direction the player is facing.
    /// It should move in the same direction (transform.right), but at speed OrbVelocity.
    /// </summary>
    private void FireOrb()
    {
        // TODO
        var orb = Instantiate(OrbPrefab, (transform.position + (1 * transform.right)), Quaternion.identity);
        Rigidbody2D orbRigidBody = orb.GetComponent<Rigidbody2D>();
        orbRigidBody.velocity = (OrbVelocity * transform.right);
    }

    /// <summary>
    /// Accelerate and rotate as directed by the player
    /// Apply a force in the direction (Horizontal, Vertical) with magnitude EnginePower
    /// Note that this is in *world* coordinates, so the direction of our thrust doesn't change as we rotate
    /// Set our angularVelocity to the Rotate axis time RotateSpeed
    /// </summary>
    void Manoeuvre()
    {
        // TODO
        var horizontalAxis = Input.GetAxis("Horizontal");
        var verticalAxis = Input.GetAxis("Vertical");
        Vector2 joystickDirection = new Vector2(horizontalAxis, verticalAxis);
        rigidBody.AddForce(EnginePower * joystickDirection);
        var rotateAxis = Input.GetAxis("Rotate");
        rigidBody.angularVelocity = (RotateSpeed * rotateAxis);
    }

    /// <summary>
    /// If this is called, we got knocked off screen.  Deduct a point!
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void OnBecameInvisible()
    {
        ScoreKeeper.ScorePoints(-1);
    }
}
