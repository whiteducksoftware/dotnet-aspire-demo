module.exports = {
    '/api': {
      target: process.env['services__apiservice__1'],
      pathRewrite: {
        '^/api': '',
      },
    },
  };
