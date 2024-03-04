/// <reference types="vitest" />
import { defineConfig, type PluginOption } from 'vite'
import react from '@vitejs/plugin-react-swc'
import tsconfigPaths from 'vite-tsconfig-paths'
import { configDefaults } from 'vitest/config'
import path from 'path'
import { visualizer } from 'rollup-plugin-visualizer'

// https://vitejs.dev/config https://vitest.dev/config
export default defineConfig({
  build: {
    rollupOptions: {
      output: {
        manualChunks: (id) => {
          if (id.includes('node_modules')) {
            if (id.includes('prime')) {
              return 'primereact'
            }

            return 'vendor' // all other package goes here
          }
        }
      }
    }
  },
  plugins: [
    react(),
    tsconfigPaths(),
    visualizer({
      template: 'treemap', // or sunburst
      open: true,
      gzipSize: true,
      brotliSize: true,
      filename: 'dist/analyse.html' // will be saved in project's root
    }) as PluginOption
  ],
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
