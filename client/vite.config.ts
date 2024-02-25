/// <reference types="vitest" />
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'
import tsconfigPaths from 'vite-tsconfig-paths'
import { configDefaults } from 'vitest/config'
import path from 'path'

// https://vitejs.dev/config https://vitest.dev/config
export default defineConfig({
  plugins: [react(), tsconfigPaths()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src/'),
      pages: path.resolve(__dirname, './src/pages'),
      hooks: path.resolve(__dirname, './src/common/hooks'),
      components: path.resolve(__dirname, './src/common/components'),
      api: path.resolve(__dirname, './src/api')
    }
  },
  test: {
    globals: true,
    environment: 'happy-dom',
    setupFiles: '.vitest/setup',
    include: ['**/test.{ts,tsx}', '**/**.test.{ts,tsx}'],
    exclude: [...configDefaults.exclude, './src/__mocks__/*'],
    alias: {
      '@': path.resolve(__dirname, './src/'),
      pages: path.resolve(__dirname, './src/pages'),
      hooks: path.resolve(__dirname, './src/common/hooks'),
      components: path.resolve(__dirname, './src/common/components'),
      api: path.resolve(__dirname, './src/api'),
      store: path.resolve(__dirname, './src/store')
    }
  }
})
