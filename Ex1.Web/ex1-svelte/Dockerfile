# stage build
FROM node:19-alpine3.16

ARG PUBLIC_API_PATH
ENV PUBLIC_API_PATH $PUBLIC_API_PATH

WORKDIR /app

# copy everything to the container
COPY . .

# clean install all dependencies
RUN npm install

# remove potential security issues
RUN npm audit fix
    
# build SvelteKit app
RUN npm run build


# stage run
FROM node:19-alpine3.16

WORKDIR /app

# copy dependency list
COPY --from=0 /app/package*.json ./

# clean install dependencies, no devDependencies, no prepare script
RUN npm ci --production --ignore-scripts

# remove potential security issues
RUN npm audit fix

# copy built SvelteKit app to /app
COPY --from=0 /app/build ./

CMD ["node", "./index.js"]