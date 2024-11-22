using Firebase;
using Firebase.Auth;
using Google;
using UnityEngine;

public class GoogleSignInManager : MonoBehaviour
{
    private FirebaseAuth auth;
    private GoogleSignInConfiguration googleSignInConfig;

    void Start()
    {
        InitializeFirebase();
        ConfigureGoogleSignIn();
    }

    /// <summary>
    /// Initializes Firebase authentication.
    /// </summary>
    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    /// <summary>
    /// Configures Google Sign-In with the required Web Client ID.
    /// Replace "YOUR_WEB_CLIENT_ID.apps.googleusercontent.com" with your Firebase project's Web Client ID.
    /// </summary>
    private void ConfigureGoogleSignIn()
    {
        googleSignInConfig = new GoogleSignInConfiguration
        {
            WebClientId = "787064593330-pv4qpgsodparbeka5jr3e5909v4433j3.apps.googleusercontent.com",
            RequestIdToken = true
        };
    }

    /// <summary>
    /// Initiates the Google Sign-In process.
    /// </summary>
    public void SignInWithGoogle()
    {
        GoogleSignIn.Configuration = googleSignInConfig;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Google Sign-In Failed: " + task.Exception);
                return;
            }

            // Get the ID Token from the Google Sign-In result
            var idToken = task.Result.IdToken;

            // Use the ID Token to authenticate with Firebase
            SignInWithFirebase(idToken);
        });
    }

    /// <summary>
    /// Uses the Google ID Token to authenticate with Firebase.
    /// </summary>
    /// <param name="idToken">The ID Token obtained from Google Sign-In.</param>
    private void SignInWithFirebase(string idToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

        auth.SignInWithCredentialAsync(credential).ContinueWith(authTask =>
        {
            if (authTask.IsFaulted || authTask.IsCanceled)
            {
                Debug.LogError("Firebase Sign-In Failed: " + authTask.Exception);
                return;
            }

            // Firebase authentication successful
            FirebaseUser newUser = authTask.Result;
            Debug.Log($"User signed in successfully: {newUser.DisplayName} ({newUser.Email})");
        });
    }
}
