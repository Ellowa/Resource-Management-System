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
  compiler: {
    removeConsole: process.env.NODE_ENV === 'production'
  },
}

module.exports = nextConfig
