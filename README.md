# Freelance Service Manager

API .NET integrada com banco de dados PostgreSQL, Azure Key Vault e publicada via Docker no Azure App Service.

## 💻 Executando localmente

1. **Subir o PostgreSQL com Docker Compose:**
   ```bash
   docker-compose up -d
   ```
   
2. **Autenticar no Azure CLI (para acessar o Key Vault):**
   ```bash
   az login
   ```

3. **Rodar a aplicação:**
   ```bash
   dotnet run
   ```

## 📦 Docker - Build e Teste Local

**Testar a imagem localmente:**
```bash
docker build -t freelance-service-manager .
docker run -p 8080:80 freelance-service-manager
```
> Obs.: Para a imagem acessar o Key Vault, é necessário que a autenticação com `az login` esteja ativa.

## 📝 Checklist de Verificação

Caso enfrente erro de credencial (ex.: `CredentialUnavailableException`), verifique se:
- No local: `az login` foi executado
- No Azure: App Service tem permissão ao Key Vault
