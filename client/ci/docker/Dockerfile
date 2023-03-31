FROM node:latest as build

COPY package.json yarn.lock ./

RUN yarn install

COPY . .

RUN yarn build

##############################################

FROM nginx:alpine as release

RUN rm -rf /usr/share/nginx/html/*

COPY --from=build /usr/src/app/build /usr/share/nginx/html

COPY ./nginx.conf /etc/nginx/nginx.conf

CMD sed -i -e 's/$PORT/'"$PORT"'/g' /etc/nginx/nginx.conf && nginx -g 'daemon off;'