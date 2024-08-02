FROM node:20
WORKDIR /app
COPY Presentation/Example.Angular/package*.json ./
RUN npm install
COPY ./Presentation/Example.Angular .
RUN npm run build --prod
RUN npm install -g @angular/cli
EXPOSE 4200
CMD ["ng", "serve", "--host", "0.0.0.0"]
