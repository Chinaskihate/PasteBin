#!/bin/sh

# Start the Vault server in the background
vault server -dev -dev-root-token-id=${VAULT_DEV_ROOT_TOKEN_ID} &

# Give the Vault server a moment to start up
sleep 5

# Wait for Vault to be ready using 'vault status'
until vault status | grep "Initialized" | grep "true" && vault status | grep "Sealed" | grep "false"; do
  echo "Waiting for Vault to be ready..."
  sleep 2
done

echo "Vault is ready. Initializing..."

env | grep -E 'PG_PASSWORD|MINIO_ADMIN_LOGIN|MINIO_ADMIN_PASSWORD|MINIO_REGION'

echo "PG_PASSWORD: ${PG_PASSWORD}"
echo "MINIO_ADMIN_LOGIN: ${MINIO_ADMIN_LOGIN}"
echo "MINIO_ADMIN_PASSWORD: ${MINIO_ADMIN_PASSWORD}"
echo "MINIO_REGION: ${MINIO_REGION}"

# Log in to Vault
vault login ${VAULT_DEV_ROOT_TOKEN_ID}

# Put secrets in Vault
vault kv put secret/DevelopmentDocker \
  redis_connection="pastebin-redis:6379" \
  db_connection="Server=pastebin-db;Port=5432;Database=MainDb;User Id=postgres;Password=${PG_PASSWORD};" \
  minio_endpoint="pastebin-minio:9000" \
  minio_access_key="${MINIO_ADMIN_LOGIN}" \
  minio_secret_key="${MINIO_ADMIN_PASSWORD}" \
  minio_region="${MINIO_REGION}"

echo "Secrets have been initialized in Vault."

# Keep the Vault server running
wait
