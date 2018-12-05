// ====================================================
// More Templates: https://www.ebenmonney.com/templates
// Email: support@ebenmonney.com
// ====================================================

import { AppPage } from './app.po';

describe('ScheduleRemake App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('should display application title: ScheduleRemake', () => {
    page.navigateTo();
    expect(page.getAppTitle()).toEqual('ScheduleRemake');
  });
});
