/** @type {import('tailwindcss').Config} */

export default {
  content: ['./src/**/*.{mjs,js,ts,jsx,tsx}'],
  theme: {
    extend: {
      transitionProperty: {
        'width': 'width'
      },
      colors: {
        "main": "rgb(42, 42, 42)",
        "accent-dark": "rgb(35, 35, 35)",
        "accent-light": "rgb(60, 60, 60)",
        "accent-super-light": "rgb(150, 150, 150)",


        "error": "rgb(255,51,51)",
        "lighter-a-bit": "#ffffff22",
      }
    },
  },
  plugins: []
}
