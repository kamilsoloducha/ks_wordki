import { SideSummary } from "pages/cards/models";

export class SideSummaryBuilder implements SideSummary {
  type = 1;
  value = "value";
  example = "example";
  comment = "comment";
  drawer = 1;
  isUsed = true;
  isTicked = false;

  withDrawer(value: number): SideSummaryBuilder {
    this.drawer = value;
    return this;
  }

  withIsUsed(value: boolean): SideSummaryBuilder {
    this.isUsed = value;
    return this;
  }

  withTicked(value: boolean): SideSummaryBuilder {
    this.isTicked = value;
    return this;
  }

  build(): SideSummary {
    return this as SideSummary;
  }
}
