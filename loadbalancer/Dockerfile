FROM nginx:alpine
RUN mkdir -p /etc/nginx/ssl

# Production certificate
COPY ./certificate/asvapp.crt /etc/nginx/ssl/asvapp.crt
COPY ./certificate/asvapp.key /etc/nginx/ssl/asvapp.key

# Localhost certificate
COPY ./certificate/localhost.crt /etc/nginx/ssl/localhost.crt
COPY ./certificate/localhost.key /etc/nginx/ssl/localhost.key
#COPY ./certificate/localhost_full.crt /etc/nginx/ssl/localhost_full.crt

COPY ./loadbalancer/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80 443
CMD ["nginx", "-g", "daemon off;"]
