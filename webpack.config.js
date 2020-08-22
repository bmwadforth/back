const path = require('path')
const webpack = require('webpack')
const nodeExternals = require('webpack-node-externals')
const NodemonPlugin = require('nodemon-webpack-plugin');

module.exports = {
    mode: 'development',
    entry: './index.js',
    target: 'node',
    externals: [nodeExternals()],
    devtool: 'source-map',
    module: {
        rules: [
            {
                test: /\.src\/?js$/,
                exclude: /(node_modules|app)/,
                use: {
                    loader: 'babel-loader?retainLines=true',
                    options: {
                        presets: ['@babel/preset-env']
                    }
                }
            }
        ]
    },
    plugins: [new NodemonPlugin()],
    output: {
        path: path.resolve(__dirname, 'dist')
    },
    
}