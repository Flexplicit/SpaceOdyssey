
Running backend (runs on 8060 port 70, do not change ports)

```
cd .\AspSolutionBackend\
docker build --label WebApp -t backend -f Dockerfile .
docker run -p 8060:70 -it --rm backend
```

Running frontend

```
cd  cd .\ReactSolutionFrontend\react-app
npm install
npm run build
npm start
```
