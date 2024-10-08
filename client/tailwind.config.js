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

        "drawer-1": "rgb(129, 0, 0)",
        "drawer-2": "rgb(129, 84, 0)",
        "drawer-3": "rgb(129, 128, 0)",
        "drawer-4": "rgb(40, 128, 5)",
        "drawer-5": "rgb(0, 54, 0)",


        "error": "rgb(255,51,51)",
        "lighter-a-bit": "#ffffff22",
      }
    },
  },
  plugins: []
}
