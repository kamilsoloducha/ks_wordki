import { convertIntoExtendedText } from "../CardItem";

fdescribe("", () => {
  [
    { input: "", expectation: [{ text: "", isLowlight: false }] },
    { input: "text", expectation: [{ text: "text", isLowlight: false }] },
    { input: "{text}", expectation: [{ text: "text", isLowlight: true }] },
    {
      input: "test{text}",
      expectation: [
        { text: "test", isLowlight: false },
        { text: "text", isLowlight: true },
      ],
    },
    {
      input: "{text}test",
      expectation: [
        { text: "text", isLowlight: true },
        { text: "test", isLowlight: false },
      ],
    },
    {
      input: "test{text}test",
      expectation: [
        { text: "test", isLowlight: false },
        { text: "text", isLowlight: true },
        { text: "test", isLowlight: false },
      ],
    },
    {
      input: "test{text}test{text}",
      expectation: [
        { text: "test", isLowlight: false },
        { text: "text", isLowlight: true },
        { text: "test", isLowlight: false },
        { text: "text", isLowlight: true },
      ],
    },
    {
      input: "{text}{text}",
      expectation: [
        { text: "text", isLowlight: true },
        { text: "text", isLowlight: true },
      ],
    },
    {
      input: "{text} {text}",
      expectation: [
        { text: "text", isLowlight: true },
        { text: " ", isLowlight: false },
        { text: "text", isLowlight: true },
      ],
    },
  ].forEach((item, index) => {
    it(" " + index, () => {
      const result = convertIntoExtendedText(item.input);
      expect(result).toStrictEqual(item.expectation);
    });
  });
});
