declare global {
  namespace jest {
    interface Matchers<R> {
      toDeepEqual: (expected: any) => any
    }
  }
}

function hasEqualStructure2(object1: any, object2: any): any {}

expect.extend({
  /**
   * Notice that this implementation has 2 arguments, but the implementation inside the Matchers only has 1
   */
  toDeepEqual(received: any, expected: any): any {
    const receivedString = JSON.stringify(received)
    const expectedString = JSON.stringify(expected)
    const pass = expectedString === receivedString

    return {
      message: () => `expected ${expectedString} to match structure ${receivedString}`,
      pass
    }
  }
})

// I am exporting nothing just so we can import this file
export default undefined
