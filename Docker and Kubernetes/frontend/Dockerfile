# This builder phase creates the artifacts for the application
FROM node:alpine as builder

WORKDIR '/app'
COPY package.json .
RUN npm install
COPY . .
RUN npm run build


FROM nginx

# Copy artifacts created from the builder phase
COPY --from=builder /app/build /usr/share/nginx/html

# Default run command of nginx image will start the nginx server.




