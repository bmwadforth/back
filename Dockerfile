FROM node:latest

# Create app directory
WORKDIR /usr/app

# Bundle app source
COPY ./dist .
COPY ./package.json .

RUN npm install

EXPOSE 8080
CMD [ "node", "/usr/app/main.js" ]