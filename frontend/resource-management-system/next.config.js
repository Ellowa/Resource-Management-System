/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  async rewrites() {
    return [
      {
        source: '/api/:path*',
        destination: 'https://resource-ms-backend.azurewebsites.net/api/:path*',
      }
    ]
  },
}

module.exports = nextConfig
