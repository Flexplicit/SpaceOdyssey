
Running backend (runs on 8060 port 80, do not change ports)

```
cd .\AspSolutionBackend\
docker build --label WebApp -t backend -f Dockerfile .
docker run -p 8060:80 -it --rm backend
```

Running frontend

```
cd .\ReactSolutionFrontend\react-app
npm install
npm start
```
