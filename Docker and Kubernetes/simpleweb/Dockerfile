# Select the base image
FROM node:alpine

WORKDIR /usr/app

COPY ./package.json ./

# Install some dependencies
RUN npm install

COPY ./ ./

# Default Command
CMD ["npm", "start"]