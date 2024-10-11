# PasswordManager
 Forget your passwords and let this program handle them for you!

# How to run the app
1. Download the source code
2. Run the following command to run the application
```
dotnet run
```
3. Now you can safely store your passwords!

# Password Manager set up
The first time you run the app you'll be prompted with the following message:

![image](https://github.com/user-attachments/assets/fefb6ee7-7190-4673-a975-9fbcd32a2352)

This alerts you that you'll be asked to introduce a Master Password that will be used to authenticate yourself the next time you try to open the application. Please remember this MasterKey as losing it would result in a total loss of your accounts information and password!

![image](https://github.com/user-attachments/assets/d530c427-b4cb-4754-9b71-7f15e4df2468)

Once you have introduced your Master Password, you'll be set to go to start adding your current accounts and updating their passwords with freshly generated ones :)

![image](https://github.com/user-attachments/assets/2e5b3987-41ae-42af-b6eb-22a9a73dde01)

# Threat actors and considerations

This application is secured agains threat actors such as:

1. External attackers, or people who gain unauthorized accces to the application.

2. Malicious insiders, or people with legitimate access to the system who abuse their privileges to extract sensitive data or compromise this app.

3. Malware and Keyloggers, such as malware that captures or attempts to steal sensitive information stored within this app.

# Security Model

This application employs several security measures to protect your sensitive data:

1. AES Encryption: All sensitive data, such as account credentials, are encrypted using AES (Advanced Encryption Standard). The encryption key is derived from the user's Master Password using PBKDF2, ensuring secure storage of passwords in the database.

2. The Master Password is hashed using the PBKDF2 key derivation function, combined with a unique salt and using the SHA-256 algorithm for security.

3. The encryption key for AES is generated from the Master Password using PBKDF2 with multiple iterations to increase security.

4. All sensitive data stored in the database is encrypted, ensuring that it cannot be accessed in raw form even if an attacker gains access to the database.

5. The app requires the user to authenticate with their Master Password upon each login. This ensures that only authorized users can access the stored passwords.

# Pitfalls

1. If the user forget its Master Password, the system doesn't allow the user to modify it and/or restart the application. The only way to restart the application is by manually deleting the .db file.
