using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Iteris.Settings.Interface;

namespace Iteris.Settings
{
    public class SecurityData : ISecurityData
    {
        private readonly IAmazonSecretsManager amazonSecretsManagerClient;
        private readonly ISecretManager secretManager;
        private readonly GetSecretValueRequest secretValueRequest;

        public SecurityData(IAmazonSecretsManager amazonSecretsManagerClient, ISecretManager secretManager)
        {
            this.amazonSecretsManagerClient = amazonSecretsManagerClient;
            this.secretManager = secretManager;

            secretValueRequest = new GetSecretValueRequest
            {
                SecretId = secretManager.SecretName,
                VersionStage = secretManager.VersionStageDefault
            };

        }

        public bool ContainsPrefix(string environmentVariable)
        {
            if (string.IsNullOrEmpty(environmentVariable)) return false;
            return environmentVariable.StartsWith(secretManager.Prefix, StringComparison.InvariantCultureIgnoreCase);
        }

        private string GetKeyName(string value)
        {
            if (!string.IsNullOrEmpty(value) && value.Contains(":"))
                return value.Split(secretManager.PrefixDelimiter)[1];
            else
                return value;
        }

        public string GetSecret(string key)
        {
            GetSecretValueResponse response = amazonSecretsManagerClient.GetSecretValueAsync(secretValueRequest).Result;


            string secretJson;
            //Descriptografa o segredo usando o KMS CMK associado.
            //Dependendo se o segredo é uma string ou binário, um desses campos será preenchido.
            if (response.SecretString != null)
                secretJson = response.SecretString;
            else
            {
                MemoryStream memoryStream = response.SecretBinary;
                StreamReader reader = new StreamReader(memoryStream);
                string decodedBinarySecret = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
                secretJson = decodedBinarySecret;
            }

            //Obtem a primeira posição do valor contido na variável
            //Exemplo: O valor contido na variável será recebido assim
            //SECRET_KEY:NOME_DA_CHAVE_DE_PESQUISA_NO_SECRET
            key = GetKeyName(key);

            Dictionary<string, string> keys = JsonConvert.DeserializeObject<Dictionary<string, string>>(secretJson);
            keys.TryGetValue(key, out string secretValue);
            return secretValue;
        }


    }
}
