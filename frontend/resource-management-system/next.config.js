/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  async rewrites() {
    return {
      fallback: [
        {
          source: '/api/:path*',
          destination: 'https://resource-ms-backend.azurewebsites.net/api/:path*',
        }
      ]
    }
  },
}

module.exports = nextConfig
