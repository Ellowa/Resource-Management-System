/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  async rewrites() {
    return [
      {
        source: '/api/Resource',
        destination: 'https://resource-ms-backend.azurewebsites.net/api/Resource',
      },
      {
        source: '/api/Request',
        destination: 'https://resource-ms-backend.azurewebsites.net/api/Request',
      },
    ]
  },
}

module.exports = nextConfig
