# Context
The `OpenAi Api` is currently in beta. In order to get access you need a private api key which is aquired by signing up for the beta at the [OpenAi Api Website](https://beta.openai.com/). When accepted you'll be able to access your api key on the [Developer QuickStart Page](https://beta.openai.com/docs/developer-quickstart)

Individuals that are part of an organization can also use an organization key. I'm not sure where this is found, as I do not have one. The organization key is used when individuals are using api quotas from multiple organizations, and they want to specify which quota to use. 

# Authenticating in OpenAi Api Unity
Authentication info is entered in the `SOAuthArgsV1` object. This Scriptble Object is a required input for all API prefabs. 

The `SOAuthArgsV1` has two modes.
   * Local File
   * String

### Local File
Local file mode pulls a json from a the local file path `~/.openai/auth.json` (Linux/Mac) or `%USERPROFILE%/.openai/auth.json` (Windows). The json should be formatted as follows:

```json
// auth.json
{
  "private_api_key":"<YOUR_KEY>",
  (optional) "organization":"<YOUR_ORGANIZATION_ID>"
}
```

The file is read and deserailized when the `OpenAiApiGatewayV1` is initialized

### String
String mode lets user input their OpenAI Api credentials as plain text strings. This is a bad idea in most cases though, because commiting this to a public repo reveals your key. 

