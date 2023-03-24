#######################################
# Build stage
#######################################
FROM node:18.14 as builder

ARG PUBLIC_API_PATH
ENV PUBLIC_API_PATH $PUBLIC_API_PATH

WORKDIR /app

# install npm dependencies (cache second)
COPY package*.json ./

RUN npm install

# copy remaining files
COPY . .

# build app
RUN npm run build

#######################################
# Serve stage
#######################################
FROM node:18.14-slim

WORKDIR /app

COPY --from=builder /app/package.json .
COPY --from=builder /app/node_modules ./node_modules
COPY --from=builder /app/build ./build

CMD ["node", "/app/build/index.js"]