# Arheisel.Email
A simple to use HTML Email sender

# Reference

## Constructor

`EmailSender(string server)`

`EmailSender(string server, int port)`

## Members

`string From` Sets the From Address

`string To` Adds a new recipient. Note: Can be set multiple times, each time adding a recipient

`string Subject` Sets the Subject of the email

`bool IsHTML` Wether or not to treat the Body as HTML

`string Body` Sets the body of the email

`System.Net.NetworkCredential Credentials` Sets the credentials for the SMTP Client

`bool EnableSSL` Specifies whether the SMTPClient should use SSL

## Functions

`void Send()` Sends the Email

# EmailHTMLBody Class

#### `string EmailHTMLBody.Build(string file, Dictionary<string, string> args)`

Loads an HTML file from Disk and replaces the keywords marked as {{keyword}} with the corresponding value in the key-value pair for every entry in the`args` Dictionary.


#### `string EmailHTMLBody.ImageToBase64(string path)`

Converts an image (jpeg, png, gif) specified in `path` to a data uri Base64 String.






