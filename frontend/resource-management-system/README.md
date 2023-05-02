## How to launch the project
Install node.js (preferrably 18.14.2 using [This](https://github.com/nvm-sh/nvm))
```bash
nvm install 18.14.2
nvm use 18.14.2
```
Install pnpm, launch the server
From powershell as admin
```bash
corepack enable
corepack prepare pnpm@latest --activate
pnpm install
pnpm run dev
```
If doesn't work, do this then retry
```bash
Set-ExecutionPolicy RemoteSigned
```