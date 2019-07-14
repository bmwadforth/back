FROM rust

WORKDIR /usr/src/bmwadforth
COPY . .

RUN rustup default nightly
RUN cargo b --release
RUN cp ./target/release/bmwadforth /usr/bin/bmwadforth
RUN rm -rf /usr/src/bmwadforth

EXPOSE 8000
CMD ["/usr/bin/bmwadforth"]